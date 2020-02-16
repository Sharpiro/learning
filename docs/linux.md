# Linux

## General

* Everything is a file

## Root Directories

* bin (binaries)
  * os binaries and programs like `cat` and `ls`
* boot
  * os boot loader
* cd-rom
  * legacy mount point for cd-rom
* dev (devices)
  * hardware devices
    * discs
    * webcams
    * keyboards
    * etc
  * first hard drive partition
    * /dev/sda1
* etc (etcetera)
  * system-wide configurations
* home
  * personal docs
  * can be installed on different drives which allows for easier re-install
* libs (libraries)
  * where libraries of functions are stored for use by binaries
  * lib
  * lib32
  * lib64
* media
  * where os managed mounted devices
  * leave this to the OS to manage
* mnt (mount)
  * where you should mount things manually
* opt (optional)
  * where manually installed software from vendors resides
  * where you can install software you've created yourself
* proc (processes)
  * information about processes
  * cpu info
    * `cat /proc/cpuinfo`
* root
  * root user's home folder
  * does not reside in `home` directory like other users
* run
  * tempfs file system
    * all in ram
    * gone on reboot
* sbin
  * binaries for root user
* snap (snap packages)
  * self-contained applications
* srv (services)
  * files served to http/ftp
* sys (system)
  * way to interact with kernel
  * also not physically written to disc like `run`
* tmp (temporary)
  * files are temporarily stored during application session
* usr (user)
  * non-essential software
  * most software installed from source ends up in local folders
* var (variable)
  * files and directories expected to grow in size
  * logs

## Environment Variables And Aliases

### Current User

#### Add Aliases (current user)

```bash
vim ~/.bashrc

# add alias
alias py=python3
```

#### Add to path (current user)

Open `~/.profile` and add new directory to ```PATH```:

```bash
export PATH="$HOME/.cargo/bin:$PATH"
```

#### Add Script or Program

Copy program to ```/usr/bin```

### All Users

#### Add Aliases (all users)

Copy ```.sh``` script to ```/etc/profile.d```

ex: ```test.sh```:

```bash
sudo vim /etc/profile.d/test.sh

# add alias
alias py=python3
```

#### Add to path (all users)

Copy ```.sh``` script to ```/etc/profile.d```

```bash
export PATH="$HOME/.cargo/bin:$PATH"
```

## Keyboard Mapping

### Terminal

```sh
# special characters section
 stty --help | less

# change "interrupt" to ctrl-k
stty intr \^k

# clear line with escape
bind '"\e":"\C-k \C-u"'

# clear screen with ctrl-r
bind '"\C-r":"\C-k \C-uclear\n"'

# alt-shift-w to execute command w/o disturbing current line
bind -x '"\eW":"who"'
```

#### Persistent Terminal Bindings

add to `~/.inputrc`

```sh
"\C-r":"\C-k \C-uclear\n"
"\e":"\C-k \C-u"
```

### Global

```sh
# create new symbols file
sudo vim /usr/share/X11/xkb/symbols/custom

# OR update default symbols file
sudo vim /usr/share/X11/xkb/symbols/us
```

```sh
# paste in the following
xkb_symbols "custom" {

 name[Group1] = "Custom";
 include "us(basic)"

        key <RALT> {        [     Home,     Home            ]       };
        key <RCTL> {        [     End,      End             ]       };
};

```

```sh
# load new symbol configuration
setxkbmap custom
```

## Disk commands

### List information

```bash
sudo fdisk -l

df

lsblk
```

### Create Windows ISO in Linux

```bash
# format filesystem NTFS

# install WoeUSB
sudo dnf install WoeUSB

# copy ISO to USB
sudo woeusb \
    --target-filesystem NTFS \
    --device Win10_1809Oct_English_x64.iso /dev/sdc
```

## Luks

### dump master key

```bash
sudo cryptsetup luksDump --dump-master-key /dev/nvme0n1p2
```

## SSH

### SFTP via Nautilus

```sh
ssh-add /home/user/path-to-your-key/key_name
sftp://funky@ip.to.your.server:7000
```

### SCP folder contents from remote to local

```sh
scp -r pi@192.168.1.2:/home/pi/gitbase/downloader/web_server/.cache .
```

## 7 Zip

* Installation
  * ```sudo apt install p7zip-full```
* Usage
  * ```7z```
  * ```x```: extract
  * ```l```: list archive
  * ```-otemp```: set output directory to 'temp'

## Links / Shortcuts

### Symbolic Links

```bash
# ln -s source target
sudo ln -s /usr/lib64/libssl.so.1.1.1 /usr/lib64/libssl.so.1.0.0
```

### add to applications menu

`~/.local/share/applications/test.desktop`

```sh
[Desktop Entry]
Name=Downloader
GenericName=File Synchronizer
Comment=Sync your files across computers and to the web
Exec=/home/user/src/app/test-0.0.0.AppImage
Icon=/home/user/src/app/icon.png
Terminal=false
Type=Application
StartupNotify=true
Encoding=UTF-8
```

> **note:** `StartupNotify` is likely only for taskbar notifications

### add application to startup

add `.desktop` file to `~/.config/autostart/`

## Ignore case in terminal

Add the following to ```~/.inputrc``` for current user or ```/etc/inputrc``` for all users:

```bash
set completion-ignore-case On
```

## Git Credential libsecret

```sh
git config --global credential.helper /usr/libexec/git-core/git-credential-libsecret
```

## OpenSSL

* gen private key only

```sh
openssl genpkey -algorithm RSA -out key.pem
```

* gen private/public key .pem files

```sh
openssl req -x509 -newkey rsa:4096 -keyout test-private-key.pem -out test-public-key.pem -nodes -subj '/CN=localhost'
```

* gen `pkcs12` .pfx file

```sh
openssl pkcs12 -export -out test-pkcs12.pfx -inkey test-private-key.pem -in test-public-key.pem -passout pass:
```

## Misc

### iPhone detection fix

 ```bash
 sudo usbmuxd -u -U usbmux
```

* <https://ubuntuforums.org/showthread.php?t=2376741>

#### Allow new directory to be used by super users

Open `/etc/sudoers` and add new directory to ```secure_path```:

```bash
secure_path="/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/snap/bin:/opt/node-v10.13.0-li
nux-x64/bin"
```

### Print Current Directory

```bash
pwd
```

### Search Text

```bash
echo data this is ata my text data | grep ata
```

```bash
grep ata test.txt
```

### Get Count of Search Text

```bash
echo data this is ata my text data | grep -o ata | wc -l
```

```bash
grep -o ata test.txt| wc -l
```

### Customize Bookmarks

```bash
# default bookmarks
vim ~/.config/user-dirs.dirs
```

```bash
# custom bookmarks
vim ~/.config/gtk-3.0/bookmarks
```

### verify a password against the stored hash

* `/etc/shadow` hash entry
  * `:` - delimiter
  * order
    * user
    * password hash
    * last changed?
    * ...

```sh
sudo cat /etc/shadow | grep test_user

# test_user:$6$rXT52XGHPU0MpT9f$n8jVgWOh.0jJiZrdAYATX.jD02Va.oNHSph05OxymFJI83w84l2X75iLsFqS2Wa/XKce3re7EaTlT8a2Zfkyo0:18246:0:99999:7:::
```

verifying the hash

* `$` - start of hash segment
* order
  * hash type
  * salt
  * hash

```sh
openssl passwd -6 -salt rXT52XGHPU0MpT9f
# $6$rXT52XGHPU0MpT9f$n8jVgWOh.0jJiZrdAYATX.jD02Va.oNHSph05OxymFJI83w84l2X75iLsFqS2Wa/XKce3re7EaTlT8a2Zfkyo0
```

## Network

### VPN settings

```sh
alias network='nm-connection-editor'
```

### Network Scan

```sh
sudo nmap -sn 192.168.1.0/24
```

### openvpn connection

```sh
sudo openvpn --config /etc/openvpn/ovpn_udp/us4111.nordvpn.com.udp.ovpn --auth-user-pass /home/pi/.openvpn/cred
```

> todo: Don't know how to remove "Starred" yet...

### Gnome Extensions

If a duplicate package exists in Software, try to use the one directly from your distro as opposed to from GNOME

* Gnome Tweaks
  * tweaks for gnome
* Hide Top Bar
  * hides the top bar when windows are maximized
* Dash to Dock
  * moves dash to bottom of screen for a more windows like experience
  * 24 px
* TopIcons Plus
  * Adds tray icons for running applications to top bar

## Dropbox

### fedora 31 dependency

```sh
sudo dnf install libatomic
```

## youtube-dl

### good test videos

<!-- cspell:disable -->
* [Audio Video Sync Test---ucZl6vQ_8Uo](https://youtu.be/ucZl6vQ_8Uo)
* [costa_rica_4k_60fps_hdr---LXb3EKWsInQ](https://youtu.be/LXb3EKWsInQ)
* [clippable cheetah video---v7p6VZiRInQ](https://youtu.be/v7p6VZiRInQ)
  * start
    * 00:00:05
  * stop
    * 00:00:15
* [long aquarium video---zJ7hUvU-d2Q](https://youtu.be/zJ7hUvU-d2Q)
  * start
    * 05:43:50
  * stop
    * 05:43:60
<!-- cspell:enable -->

### socks 5 proxy

```sh
youtube-dl --proxy socks5://127.0.0.1:9050 https://youtu.be/url
```

### get direct video url

Gets the temporary, direct, downloadable, link for a video to be consumed by other clients

```sh
youtube-dl -f 22 -g  https://youtu.be/url
```

### download entire channel (all videos playlist?)

* select best video and best mp4 compatible audio format (m4a)
* select output merge format (mp4 for web)
* force continue a partially downloaded video (?)
* use template to create video-related file name
* ignore errors (?)
* do not overwrite files
* do not re-download previously downloaded files (even if they are subsequently deleted or renamed)
* show verbose details

```sh
youtube-dl -f bestvideo+m4a --merge-output-format mp4 -ciw \
--download-archive downloaded.txt -o "%(upload_date)s-%(id)s-%(title)s.%(ext)s" -v \
https://www.youtube.com/channel/channel_id
```

### download highest quality browser compatible video

```sh
youtube-dl -f bestvideo+m4a --merge-output-format mp4 video_id
```

### formats

* best audio/video pre-packaged video (max 720p?)
  * `-f best`
* best video only (no audio)
  * `-f bestvideo`
* best video and audio and then combine them (default)
  * `-f bestvideo+bestaudio`
* best video and best audio that can be combined in an mp4 (see prev section)
  * `f bestvideo+m4a`
* 360p audio/video pre-packaged
  * `-f 18`
* 720p (best audio/video pre-packaged?)
  * `-f 22`

### misc

* video format query parameter in direct url
  * `itag=${format}`
* force output format to be merged into `mp4` instead of probably `mkv`
  * `--merge-output-format mp4`
