
$("#btn-delete-room-type").click(function () {
    let data_id = $(this).attr("data-id")

    $("#a-link-delete-room-type").attr("href", `/admin/RoomType/delete/?id=${data_id}`)
});


$(document).ready(() => {
    $(".paginate_button.next").text("trang tiep theo ")
    $(".paginate_button.previous").text("trang truoc ")
    $(".dataTables_info").attr("style", "display:none")

})


//play audio

const playAudio = () => {
    // Create an AudioContext
    const audioContext = new (window.AudioContext || window.webkitAudioContext)();

    // Create an AudioBufferSourceNode
    const source = audioContext.createBufferSource();

    // Construct the URL to the MP3 file in the wwwroot folder
    const audioFileUrl = '/audio/notice.wav'; // Replace with the actual path

    // Fetch the MP3 file
    fetch(audioFileUrl)
        .then(response => response.arrayBuffer())
        .then(data => audioContext.decodeAudioData(data))
        .then(buffer => {
            // Set the buffer as the source's audio buffer
            source.buffer = buffer;

            // Connect the source to the audio context's destination (speakers)
            source.connect(audioContext.destination);

            // Play the audio
            source.start(0);
        })
        .catch(error => {
            console.error('Error loading audio file: ' + error);
        });

}



const getOrder = () => {
    $.get('/admin/order/CountOrder',
        (data) => {

            


           
            localStorage.setItem('tempTimes', data)
            let check = localStorage.getItem("orderTimes");
            if (check) {
                if (Number(check) < Number(data)) {
                 playAudio()
                    if (!$('.notice_newOrder').find('div').length>0) {
                        $('.notice_newOrder').append(`<div class="bg bg-danger" style="width:10px;height:10px;border-radius:50%; position:relative;bottom:40px;left:20px"></div>`)
                    }
                    
                }
            } else {
                localStorage.setItem('orderTimes', data)
            }

        }
            )
      
}
const recallData = setInterval(() => {
    getOrder();
},30000)


$(document).ready(() => { recallData})


$('.notice_newOrder').click(() => {
    location.href='/admin/order/index'
})


