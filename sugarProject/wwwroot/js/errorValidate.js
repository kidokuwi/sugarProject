function checkEmail() {
    let error = "";
    let errorEmail = document.getElementById("reg_errormail");
    const emailElement = document.getElementById("reg_email");
    const email = emailElement.value;
    checkAtSymbol(email);
    checkDot(email);
    checkLen(email);
    validateBadChars(email);
    validateHebrew(email);
    if (checkAtSymbol(email) && checkDot(email) && checkLen(email) &&
        validateBadChars(email) && validateHebrew(email)) {
        errorEmail.innerHTML = "";
        emailElement.classList.remove("is-invalid"); // this for bootstrap things
        emailElement.classList.add("is-valid");
        return true;
    } else {
        emailElement.classList.add("is-invalid");
        emailElement.classList.remove("is-valid");
        return false;
    }
    return (checkAtSymbol(email) && checkDot(email) && checkLen(email) &&
        validateBadChars(email) && validateHebrew(email))
}
function checkLen(str1) {
    let errorEmail = document.getElementById("reg_errormail");
    if (str1.length > 6) {
        return true;
    }
    errorEmail.innerHTML = "length";
    return false;
}
function checkAtSymbol(str1) {
    let errorEmail = document.getElementById("reg_errormail");
    if ((str1.indexOf("@") == str1.lastIndexOf("@")) && str1.indexOf("@") != -1
        && str1.indexOf("@") != 0 && str1.indexOf("@") != str1.length) {
        return true;
    }
    errorEmail.innerHTML = "At sign @";
    return false;
}
function checkDot(str1) {
    let errorEmail = document.getElementById("reg_errormail");
    if (str1.lastIndexOf(".") > str1.indexOf("@") + 1) {
        if (str1.indexOf(".") != str1.length - 1) {
            return true;
        }
    }
    errorEmail.innerHTML = "dot";
    return false;
}
function validateHebrew(str1) {
    let errorEmail = document.getElementById("reg_errormail");
    let l = str1.length;
    for (let i = 0; i < l; i++) {
        if (str1.charAt(i) >= 'א' && str1.charAt(i) <= 'ת'){
            errorEmail.innerHTML = "no hebrew";
            return false;
        }
      
    }
    return true;
}
function validateBadChars(str1) {
    let errorEmail = document.getElementById("reg_errormail");
    let badCh = "!#$%^&*()-<>[]{}?+=~"
    let l = badCh.length;
    for (let i = 0; i < l; i++) {
        for (let j = 0; j < str1.length; j++) {
            if (str1.charAt(j) == badCh.charAt(i)) {
                errorEmail.innerHTML = "bad chars";
                return false;
            }
        }
    }
    return true;
}