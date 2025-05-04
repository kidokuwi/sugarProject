function enlarge(imgNum) {
    let img = "b" + imgNum;
    let mainImg = document.getElementById("mainImg");
    let imgSrc = "/imgs/BreadTypes/" + img + ".jpg";


    // rm img
    mainImg.classList.add("pop-in");

    // after 0.1s change img and appear
    setTimeout(() => {
        mainImg.src = imgSrc;
        mainImg.classList.remove("pop-in");
    }, 100); 
}