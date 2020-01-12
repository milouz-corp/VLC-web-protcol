# VLC-web-protcol
a windows executable allowing opening vlc:// link in browser with VLC

## Prerequisite

Work on Windows 32/64bits

VLC need to be installed (32/64bits) on the default Program Files folder

## How it works

Download the executable, run it once as Administrator to do the setup. it will automatically do the following steps :

-copy itself in the VLC folder

-register the vlc:// protocol

-add it to the chrome whitlist to remove the annoying  (each time) confirmation.

## Binary

You can dowload the lateset released here :
[Download](https://github.com/milouz-corp/VLC-web-protcol/releases/latest/download/VLC-web-protocol.exe)

then try the following link :

```html
<a href='vlc://http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4'>Link1</a>
```

you can also add args separated by the url encoded space "%20" :

```html
<a href='vlc://http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4%20--start-time=83.4'>Link2</a>
```
 try it live [HERE](http://htmlpreview.github.io/?https://github.com/milouz-corp/VLC-web-protcol/edit/master/sample.html)
