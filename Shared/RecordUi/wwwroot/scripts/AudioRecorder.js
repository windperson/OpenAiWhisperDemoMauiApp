var BlazorAudioRecorder = {};
(function () {
    let mStream;
    let mMediaRecorder;
    let mCaller;
    let audioBlob;

    BlazorAudioRecorder.Initialize = function (vCaller) {
        mCaller = vCaller;
    };

    BlazorAudioRecorder.StartRecord = async function () {
        mStream = await navigator.mediaDevices.getUserMedia({
            audio: {
                channelCount: 1,
                sampleRate: 16000,
                sampleSize: 16,
                volume: 1
            }
        });
        mMediaRecorder = new MediaRecorder(mStream, { mimeType: "audio/webm" });
        let mAudioChunks = [];
        mMediaRecorder.addEventListener('dataavailable', vEvent => {
            mAudioChunks.push(vEvent.data);
        });
        mMediaRecorder.addEventListener('stop', () => {
            audioBlob = new Blob(mAudioChunks, { type: "audio/webm" });
            let pAudioUrl = URL.createObjectURL(audioBlob);

            // noinspection JSUnresolvedFunction
            mCaller.invokeMethodAsync('OnAudioUrl', pAudioUrl);
        });
        mMediaRecorder.addEventListener('error', vError => {
            console.warn('media recorder error: ' + vError);
        })

        mMediaRecorder.start();
    };
    BlazorAudioRecorder.PauseRecord = function () {
        mMediaRecorder.pause();
    };
    BlazorAudioRecorder.ResumeRecord = function () {
        mMediaRecorder.resume();
    };
    BlazorAudioRecorder.StopRecord = function () {
        mMediaRecorder.stop();
        mStream.getTracks().forEach(pTrack => pTrack.stop());
    };

    // Extract raw data for Blazor to create multipart-form request
    BlazorAudioRecorder.GetRawData = async function () {
        const arrayBuffer = await audioBlob.arrayBuffer();
        return new Uint8Array(arrayBuffer);
    };
})();
