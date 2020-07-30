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

```sh
sudo fdisk -l

df

lsblk
```

### Create Windows ISO in Linux

```sh
# format filesystem NTFS

# install WoeUSB
sudo dnf install WoeUSB

# copy ISO to USB
sudo woeusb \
    --target-filesystem NTFS \
    --device Win10_1809Oct_English_x64.iso /dev/sdc
```

## Encryption

### Luks

#### setup and format luks partition

note: you may want to setup a normal partition using `parted` prior to this to give you a partition accessible at something like `/dev/sda1` rather than using the disk as in `/dev/sda`, but I don't see any advantage at this time since we want the entire disk encrypted

```sh
cryptsetup luksFormat /dev/sda
cryptsetup open /dev/sda encrypted
mkfs.ext4 /dev/mapper/encrypted
```

optionally modify ownership after encrypted partition is mounted

```sh
chown bob.bob /home/bob/dev_mounts/mnt
```

#### open and mount encrypted partition

```sh
# decrypt (encrypted_partition, decrypted_partition_name)
sudo cryptsetup open /dev/sda1 decrypted_partition
# decrypted partition symblinks are found @ /dev/mapper
# mount (decrypted_partition_symlink, mountdir)
sudo mount /dev/mapper/decrypted_partition ~/temp/temp_mount_dir
```

#### un-mount and close encrypted partition

```sh
#un-mount (mountdir)
sudo umount ~/temp/temp_mount_dir
# decrypt (decrypted_partition_name)
sudo cryptsetup close decrypted_partition
```

#### dump master key

```sh
sudo cryptsetup luksDump --dump-master-key /dev/nvme0n1p2
```

### `gocryptfs`

#### mount encrypted folder

```sh
gocryptfs cipher_dir mount_dir
```

#### mount encrypted folder with gnome secrets script

```sh
cipher_dir=$(dirname "$0")/gcfs_cipher
mount_dir=~/run/media/gcfs_mount
gnome_key_path="gocryptfs sub"

mkdir -p $mount_dir
gocryptfs --extpass="secret-tool lookup $gnome_key_path" $cipher_dir $mount_dir
```

## SSH

### login using ssh keys

copy public key(s) to target machine

```sh
ssh-copy-id user@host
```

disable password authentication

```sh
# /etc/ssh/sshd_config
PasswordAuthentication no
ChallengeResponseAuthentication no
UsePAM no
```

### SFTP via Nautilus

```sh
ssh-add /home/user/path-to-your-key/key_name
sftp://funky@ip.to.your.server:7000
```

### SCP folder contents from remote to local

```sh
scp -r pi@192.168.1.2:/home/pi/gitbase/downloader/web_server/.cache .
```

### local port forwarding

Allows the forwarding of local traffic to ports on remote machines.

* uses
  * bypass firewalls (only ssh port needs to be open)
  * encrypt traffic that would otherwise not be (http)
  * trick applications into thinking they're connecting to local ports/services

```sh
ssh -fNT -L localhost:8080:remote_machine:8080 remote_machine
```

* `-fNT` - run detached, disable commands, disable terminal allocation
* `-L` - forward requests to localhost:port to remote_machine:port.

## Links / Shortcuts

### Symbolic Links

```sh
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

## startup locations

* login shell // todo: test
  * ~/.bash_profile
  * ~/.profile
  * /etc/profile
  * /etc/profile.d
  * add `.desktop` file to `~/.config/autostart/`
* startup
  * @reboot in crontab

## bash

### shell parameters

[special parameters](https://www.gnu.org/savannah-checkouts/gnu/bash/manual/bash.html#Special-Parameters)

* `$0`
  * current shell script name or just shell name if not a script
* `$<1..x>`
  * a positional parameters provided to the script
* `$*` or `$@`
  * all positional parameters provided to the script
  * `"$*"` surrounds all parameters in a quote group
  * `"$@"` surrounds each parameter in quote groups
* `$#`
  * gets the count of positional parameters
* `$?`
  * exit status of most recent command
* `$$`
  * process id of shell
* `$!`
  * process id of most recent job placed in background

### Ignore case

Add the following to ```~/.inputrc``` for current user or ```/etc/inputrc``` for all users:

```sh
set completion-ignore-case On
```

### aliases

[auto-complete only seems to work on debian](https://unix.stackexchange.com/a/224228/297767)

```sh
alias pm=podman
complete | grep podman
complete -F _cli_bash_autocomplete pm
```

### path

```sh
export PATH="$HOME/.cargo/bin:$PATH"
```

### get script directory

```sh
cipher_dir=$(dirname "$0")/gcfs_cipher
```

### redirect stdout and stderr to file

```sh
# shorthand for: cat test_file > /dev/null 2>&1
cat test_file &> /dev/null
```

### if statement

```sh
cat test_file

# double parens allow for more familiar logical operators
if (($? == 0)); then
  echo "'test_file' exists"
else
  echo "'test_file' does not exist"
fi
```

### flag parameters

```sh
# script.sh
while getopts u:a:f: flag
  do
    case "${flag}" in
      u) username=${OPTARG};;
      a) age=${OPTARG};;
      f) fullname=${OPTARG};;
    esac
  done
echo "Username: $username"
echo "Age: $age"
echo "Full Name: $fullname"

# usage
./script.sh -a 99 -u test -f a
```

## openssl

### definitions

* pkcs1
  * cert format for rsa
* pkcs7
  * cryptographic message syntax (uncommon)
* pkcs8
  * like pkcs1 but for any algorithm
  * typical cert format
* pkcs12
  * pfx encrypted format
* der
  * binary cert format
* pem
  * base64 encoding of `der` format

### links

* [What data is saved in RSA private key?](https://crypto.stackexchange.com/a/7964)
* [PKCS#1 and PKCS#8 format for RSA private key](https://stackoverflow.com/q/48958304/5344498)

### gen private key only

#### gen pkcs1 private key (?) `BEGIN RSA PRIVATE KEY`

```sh
openssl genrsa -out key.pem 2048
```

#### gen pkcs8 private key (?) `BEGIN PRIVATE KEY`

```sh
openssl genpkey -algorithm RSA -out key.pem -pkeyopt rsa_keygen_bits:2048
```

### gen private key and x509 certificate

```sh
openssl req -x509 -newkey rsa:4096 -keyout test-private-key.pem -out test-public-key.pem -nodes -subj '/CN=localhost'
```

### gen `pkcs12` .pfx file

```sh
openssl pkcs12 -export -out test-pkcs12.pfx -inkey test-private-key.pem -in test-public-key.pem -passout pass:
```

### convert from pkcs1 to pkcs8

```sh
openssl pkcs8 -topk8 -in private_key_pkcs1.pem -nocrypt -out private_key_pkcs8.pem
```

### create rsa public key from private key

```sh
openssl rsa -in private_key.pem -pubout -out public_key.pem
```

### view rsa private key info

```sh
openssl rsa -text -noout -in key.pem
```

### view rsa public key info

```sh
# replace 'rsa' with 'pkey' for other key types
openssl rsa -pubin -text -in pub.pem
```

### view x509 cert info

```sh
openssl x509 -text -noout -in cert.cer
```

### extract public key from x509

```sh
openssl x509 -pubkey -noout -in cert.pem  
```

### extract cert from pfx (pkcs12)

```sh
openssl pkcs12 -in in.pfx -clcerts -nokeys -out out.crt
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

## PGP/GPG

### sign a doc (detached)

```sh
gpg --detach-sign -armour file
```

### verify a signature

```sh
gpg --verify file.asc
```

### view signature info

```sh
pgpdump file.asc
```

```sh
gpg --list-packets < file.asc
```

## administration

### change hostname

update hostname in both of the below files

```sh
sudo vim /etc/hosts
sudo vim /etc/hostname
sudo reboot
```

### add user

`adduser` will properly setup home directories and files by default whereas `useradd` will not

```sh
sudo adduser bob
```

### add user to group

```sh
sudo adduser bob sudo
```

### modify sudo permissions

modify the `/etc/sudoers` file or a file it references by specifying it specifically

```sh
sudo visudo
```

### disable raspberry pi no password sudo permissions

comment out the lines in the below file or perhaps just delete it

```sh
sudo visudo "/etc/sudoers.d/010_pi-nopasswd"
```

### change user password

change a user's password or the current user's password if no user is specified

```sh
sudo passwd bob
```

### remove/delete user

```sh
sudo deluser --remove-home bob
```

### disable wireless/bluetooth devices/interfaces

```sh
sudo rfkill block wlan bluetooth
```

## vim

### default config

* `/etc/vim/vimrc`
* `~/.vimrc`

### my default .vimrc

```sh
source $VIMRUNTIME/defaults.vim
set mouse-=a
```

### disable visual mouse mode

This mode breaks copy/paste when in an ssh session.

* **Any** of the following can fix it.
  * press `shift` while selecting with mouse to bypass
  * execute `:set mouse-=a` in vim to disable
  * comment out `set mouse=a` setting in `vimrc`
  * add `set mouse-=a` setting in `vimrc`

### binary conversion

* `:%!xxd`
  * convert text to binary
* `:%!xxd -r`
  * convert binary to text
* `-p`
  * modify xxd to use "plain" style for continuous bytes w/ no line numbers or columns

### misc vim commands

* `:echo $VIMRUNTIME`
  * get vim env variables while running

## podman

### volumes

* a volume mount to a file is equivalent to copying from host to container on start
  * useful for configs that shouldn't change until a container is restarted
* a volume mount to a directory is equivalent to sharing the directory
  * useful for files and subdirectories that needs to be constantly updated
  * useful when multiple containers are sharing files to keep them in sync

## Misc

### iPhone detection fix

 ```sh
 sudo usbmuxd -u -U usbmux
```

* <https://ubuntuforums.org/showthread.php?t=2376741>

#### Allow new directory to be used by super users

Open `/etc/sudoers` and add new directory to ```secure_path```:

```sh
secure_path="/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/snap/bin:/opt/node-v10.13.0-li
nux-x64/bin"
```

### Print Current Directory

```sh
pwd
```

### Search Text

```sh
echo data this is ata my text data | grep ata
```

```sh
grep ata test.txt
```

### Get Count of Search Text

```sh
echo data this is ata my text data | grep -o ata | wc -l
```

```sh
grep -o ata test.txt| wc -l
```

### Customize Bookmarks

```sh
# default bookmarks
vim ~/.config/user-dirs.dirs
```

```sh
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

### Git Credential libsecret

```sh
sudo dnf install git-credential-libsecret
git config --global credential.helper /usr/libexec/git-core/git-credential-libsecret
```

### 7 Zip

* Installation
  * ```sudo apt install p7zip-full```
* Usage
  * ```7z```
  * ```x```: extract
  * ```l```: list archive
  * ```-otemp```: set output directory to 'temp'

### check if os is 32 bit or 64 bit

```sh
getconf LONG_BIT
```

### find all text files

```sh
find . -type f -exec grep -Iq . {} \; -print
```

### find text in text files

calling grep first allows us by default to have the filename printed along with the matches.
This is useful for searching the text of many files.

```sh
grep -iI "search text" $(find . -type f | xargs)
```
