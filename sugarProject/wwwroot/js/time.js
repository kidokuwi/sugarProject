function displayTime() {
    let date = new Date();
    let currentTime = "";
    let hours = date.getHours();
    let minutes = date.getMinutes();
    let seconds = date.getSeconds();
    if (hours < 10) {
        hours += "0" + hours;
    }
    if (minutes < 10) {
        minutes += "0" + minutes;
    }
    if (seconds < 10) {
        seconds += "0" + seconds;
    }
    date = date.getDate() + "/" + (date.getMonth()+1) + "/" + date.getFullYear();
    currentTime += hours + ":" + minutes + ":" + seconds + "  " + date;
    document.getElementById("time").innerHTML = currentTime;
}