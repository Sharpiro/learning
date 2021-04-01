# ffmpeg

## basic

```sh
ffmpeg -i input.mp4 output.avi
```

## iTunes mp3

```sh
ffmpeg -i input.mp3 -aq 0 -vn output.mp3
ffmpeg -i input.mp3 -b:a 128 -vn output.mp3 -b:a 128=audio bitrate
```

## playback speed

```sh
ffmpeg -i videoplayback.m4a -af "atempo=1.5" -vn videoplayback-fast.mp3
```

## clip

```sh
# clip a 10 minute video starting at the 30 minute mark
# timestamp format: HH:MM:SS.xxx
ffmpeg -i "input.mp4" -ss 00:30:00.0 -t 00:10:00.0 "output.mp4"
```

## fade in/out

```sh
# clip w/ fade in/out
ffmpeg -i input.mp4 -ss 00:00:00.0 -t 00:00:10.0 -vf fade=in:0:60,fade=out:240:30 -af afade=in:st=0:d=1,afade=out:st=5:d=5 slide_fade_in.mp4
```

## rotation

```sh
ffmpeg -i IMG_0434.MOV -ss 15 -vf "transpose=cclock" test-rotate.mp4
```

## concatenation with same codec

glob pattern / no file

```sh
ffmpeg -f concat -safe 0 -i <(for f in ./p*.mp4; do echo "file '$PWD/$f'"; done) -c copy output.mp4
```

clips file

```sh
ffmpeg -f concat -safe 0 -i clips.txt -c copy test-combine.mp4
```

## scaling

### scale to size

```sh
ffmpeg -i input.mov -s 1920x1080 -c:a copy output.mov
```

```sh
ffmpeg -i start.MOV -vf scale=568:320 test-scale.mp4
```

### scale w/ black borders

```sh
ffmpeg -i start.MOV -vf "scale=568:320:force_original_aspect_ratio=decrease,pad=568:320:(ow-iw)/2:(oh-ih)/2" test-scale.mp4
```

## download portion of file and clip it

```sh
ffmpeg -ss 10 -i $(youtube-dl -f 22 -g  https://youtu.be/url) -t 5 test_out.mp4
```

## Pipe to stdout

**warning**: all "streaming" outputs are currently incompatible with twitter video upload

- `-f ismv`
  - similar to `mp4` but is optimized for streaming to stdout rather than a file

**warning**: `ismv` causes audio sync/corruption issues

```sh
ffmpeg -i input.mp4 -f ismv - > test_out_stream.mp4
```

**warning**: causes invalid video duration in vlc

```sh
-movflags frag_keyframe+empty_moov
```

## combine audio and video

```sh
ffmpeg -loglevel repeat+info -i file:video.mp4 \
-i file:audio.m4a -c copy -map 0:v:0 -map 1:a:0 file:test_out.mp4
```

## increase video and audio speed

```sh
# 1.5 increase
ffmpeg -i input.mp4 -filter_complex "[0:v]setpts=0.6666666666666666*PTS[v];[0:a]atempo=1.5[a]" -map "[v]" -map "[a]" output.mp4

# 1.25 increase
ffmpeg -i input.mp4 -filter_complex "[0:v]setpts=0.8*PTS[v];[0:a]atempo=1.25[a]" -map "[v]" -map "[a]" output.mp4
```

## modify video to use h.264 codec

Allows for video to be converted to a format acceptable to twitter.

Twitter requirements:

- `mp4` container
- `H264` video codec
- `AAC` audio codec
- `512MB` size limit
- `00:02:20` duration limit

```sh
ffmpeg -i input.mp4 -vc 264 out-264.mp4
```

## overlays

### single blur overlay

```sh
ffmpeg -i input.mp4 -filter_complex \
 "[0:v]crop=75:40:305:365,boxblur=10[fg]; \
  [0:v][fg]overlay=305:365[v]" \
-map "[v]" -map 0:a output.mp4
```

### multi blur overlay

```sh
ffmpeg -i input.mp4 -filter_complex \
  "[0:v]crop=75:40:525:400,boxblur=10[b0]; \
   [0:v]crop=75:40:305:365,boxblur=10[b1]; \
   [0:v][b0]overlay=525:400[ovr0]; \
   [ovr0][b1]overlay=305:365[ovr1]" \
-map "[ovr1]" -map 0:a output.mp4
```

### single box overlay

```sh
ffmpeg -i input.mp4 -vf \
  "drawbox=x=0:y=405:w=640:h=35:color=#333632@1.0:t=fill" \
   output.mp4
```

### multi box overlay

```sh
ffmpeg -i input.mp4 -filter_complex \
  "[0:v]drawbox=x=140:y=375:w=420:h=30:color=#333632@1.0:t=fill[b0]; \
   [b0]drawbox=x=0:y=400:w=640:h=40:color=#333632@1.0:t=fill[b1]" \
   -map "[b1]" -map 0:a output.mp4
```

### image overlay

```sh
ffmpeg -i input.mp4 -i overlay.png \
  -c:a copy \
  # move image to lower right and only for specified duration
  # -filter_complex "[0:v][1:v] overlay=W-w:H-h:enable='between(t,0,20)'" \
  -filter_complex "[0:v][1:v] overlay=0:158" \
  out.mp4
```

## burn file to dvd w/ autoplay

all tools are available via linux distro

[reference](https://evilshit.wordpress.com/2015/08/10/how-to-create-a-video-dvd-with-command-line-tools)

```sh
# make file dvd compatable
ffmpeg -i input.mov -target ntsc-dvd -aspect 16:9 dvd_out.mpg
# make appropriate DVD folders
dvdauthor -o dvdauthor_export/ -t dvd_out.mpg
# setup DVD folders & data to be in North America DVD format with autoplay
export VIDEO_FORMAT=NTSC && dvdauthor -o dvdauthor_export/ -T
# create an ISO image from the DVD folders
genisoimage -dvd-video -V "video_title" -o output.iso dvdauthor_export/
# burn the ISO image to a DVD writer device (check devices)
growisofs -dvd-compat -Z /dev/sr0=output.iso
```

## get file info

```sh
# print file format and streams in json format to stdout
ffprobe -v quiet -print_format json -show_format -show_streams big_test_vid.mp4
```

## stream file to nginx RTMP server

Command to have HLS stream available at
`https://server.domain.tld/live/streamkey/index.m3u8` or `https://server.domain.tld/live/streamkey.m3u8` depending on if `hls_nested` option

### stream w/o re-encoding

```sh
ffmpeg -re -i vid.mp4 -c copy -f flv rtmps://[username]:[password]@server.domain.tld:1934/publish/streamkey
```

### keyframe interval = 1

`-g 24` would be give a key frame interval of 1 for a 24 fps video

```sh
ffmpeg -re -i vid.mp4 -vcodec libx264 -g 24 -acodec copy -f flv rtmps://server.domain.tld:1934/publish/streamkey
```

## flags

| flag | usage            | description                                                                     |
| ---- | ---------------- | ------------------------------------------------------------------------------- |
| -aq  | -aq              | audio quality(lower better, fixes common iTunes length bug)                     |
| -vn  | -vn              | disable video(fixes 'invalid pixel' bug even on some audio files in rare cases) |
| -s   | -s 640x480       | resolution                                                                      |
| -af  | -af "atempo=1.5" | audio format for changing playback speed                                        |
| -re  | -re              | used to stream a video file                                                     |
| -bsf | -bsf             | apply a bitstream filter                                                        |
| -g   | -g               | keyframe interval / Group of Pictures (GOP) length                              |
| -c   | -c copy          | (codec) - in this case copy audio & video codecs and do not re-encode           |
| -c:a | -c:a copy        | (codec audio) - do not re-encode audio                                          |
| -f   | -f flv           | output video format                                                             |
