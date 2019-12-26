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
ffmpeg -i input.mp4 -ss 00:00:00.0 -t 00:00:10.0 -y -vf fade=in:0:60,fade=out:240:30 -af afade=in:st=0:d=1,afade=out:st=5:d=5 slide_fade_in.mp4
```

## rotation

```sh
ffmpeg -y -i IMG_0434.MOV -ss 15 -vf "transpose=cclock" test-rotate.mp4
```

## concatenation with same codec

```sh
ffmpeg -y -f concat -safe 0 -i clips.txt -c copy test-combine.mp4
```

## scaling

### scale to size

```sh
ffmpeg -y -i start.MOV -vf scale=568:320 test-scale.mp4
```

### scale w/ black borders

```sh
ffmpeg -y -i start.MOV -vf "scale=568:320:force_original_aspect_ratio=decrease,pad=568:320:(ow-iw)/2:(oh-ih)/2" test-scale.mp4
```

## download portion of file and clip it

```sh
ffmpeg -ss 10 -i $(youtube-dl -f 22 -g  https://youtu.be/url) -t 5 test_out.mp4
```

## Pipe to stdout

**warning**: all "streaming" outputs are currently incompatible with twitter video upload

* `-f ismv`
  * similar to `mp4` but is optimized for streaming to stdout rather than a file

**warning**: ismv causes audio sync/corruption issues

```sh
ffmpeg -i input.mp4 -f ismv - > test_out_stream.mp4
```

**warning**: causes invalid video duration in vlc

```sh
-movflags frag_keyframe+empty_moov
```

## flags

| flag          | usage             | description
| ------------- |------------------ |-------------
| -aq           | -aq               | audio quality(lower better, fixes common iTunes length bug)
| -vn           | -vn               | disable video(fixes 'invalid pixel' bug even on some audio files in rare cases)
| -s            | -s 640x480        | resolution
| -af           | -af "atempo=1.5"  | audio format for changing playback speed