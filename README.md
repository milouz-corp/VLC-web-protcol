# VLC-web-protcol
a windows executable allowing opening vlc:// link in browser with VLC

##Prerequisite

Work on Windows 32/64bits
VLC need to be installed (32/64bits) on the default Program Files folder

##How it works

Download the executable, run it once as Administrator to do the setup. it will automatically do the following steps :
-copy itself in the VLC folder
-register the vlc:// protocol
-add it to the chrome whitlist to remove the annoying  (each time) confirmation.

##Binary

You can dowload the released version here :
[Download](https://github.com/milouz-corp/VLC-web-protcol/releases/download/1/VLC-web-protocol.exe)

then try the following link :

(vlc://http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4)

you can also add args separated by the url encoded space "%20" :

(vlc://http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4%20--start-time=83.4)
