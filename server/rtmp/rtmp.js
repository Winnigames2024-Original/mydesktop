'use strict';
/*
 * MyDesktop RTMP/HLS — порты 1936 (RTMP) и 8001 (HTTP/HLS).
 * ОТДЕЛЬНО от nare-rtmp (который слушает 1935). Общего ничего нет.
 *
 * Приложение пушит:  rtmp://151.245.217.104:1936/live/<key>
 * Смотреть:
 *   HLS   :  http://151.245.217.104:8001/live/<key>/index.m3u8
 *   FLV   :  http://151.245.217.104:8001/live/<key>.flv
 */

const NodeMediaServer = require('node-media-server');

const FFMPEG = process.env.MYDESKTOP_FFMPEG || '/usr/bin/ffmpeg';

const config = {
  rtmp: {
    port: parseInt(process.env.MYDESKTOP_RTMP_PORT || '1936', 10),
    chunk_size: 60000,
    gop_cache: true,
    ping: 30,
    ping_timeout: 60
  },
  http: {
    port: parseInt(process.env.MYDESKTOP_HLS_PORT || '8001', 10),
    mediaroot: process.env.MYDESKTOP_MEDIAROOT || '/opt/mydesktop/media',
    allow_origin: '*'
  },
  trans: {
    ffmpeg: FFMPEG,
    tasks: [
      {
        app: 'live',
        hls: true,
        hlsFlags: '[hls_time=2:hls_list_size=4:hls_flags=delete_segments]',
        dash: false
      }
    ]
  }
};

const nms = new NodeMediaServer(config);

nms.on('preConnect', (id, args) => console.log('[rtmp] preConnect', id));
nms.on('postPublish', (id, streamPath, args) => console.log('[rtmp] publish', streamPath));
nms.on('donePublish', (id, streamPath, args) => console.log('[rtmp] donePublish', streamPath));

nms.run();
console.log(`[mydesktop-rtmp] RTMP :${config.rtmp.port}  HLS/HTTP :${config.http.port}  ffmpeg=${FFMPEG}`);
