::Script to generate the virtual background
cd C:\FakeVirtualBGTemp\
ffmpeg -i original.mp4 -vf reverse reverse.mp4
ffmpeg -i original.mp4 -c copy -bsf:v h264_mp4toannexb -f mpegts input1.ts
ffmpeg -i reverse.mp4 -c copy -bsf:v h264_mp4toannexb -f mpegts input2.ts
ffmpeg -i "concat:input1.ts|input2.ts" -c copy -an output.mp4