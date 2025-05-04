function enlarge(imgNum) {
    let img = "b" + imgNum;
    let mainImg = document.getElementById("mainImg");
    let imgSrc = "/imgs/BreadTypes/"+img+".jpg"
    mainImg.src = imgSrc;
}
