function checkEmail() {
    const emailElement = document.getElementById("reg_email");
    const email = emailElement.value;
    let errorEmail = document.getElementById("reg_errormail");
    if (checkAtSymbol(email) && checkAtSymbol(email) && checkLen(email) && validateBadChars(email) && validateHebrew(email)) {
        errorEmail.innerText = "";
    }
    else {
        errorEmail.innerText = "bad email";
    }
    return (checkAtSymbol(email) && checkAtSymbol(email) && checkLen(email) && validateBadChars(email) && validateHebrew(email))

}


function checkLen(str1) {
    if (str1.length > 6) {
        return true;
    }
    return false;
}
function checkAtSymbol(str1) {
    if ((str1.indexOf("@") == str1.lastIndexOf("@")) && str1.indexOf("@") != -1 && str1.indexOf("@") != 0 && str1.indexOf("@") != str1.length()-1) {
        return true;
    }
    return false;
}
function checkDot(str1) {
    if (str1.indexOf(".") > str1.indexOf("@") + 1) {
        if (str1.indexOf(".") != str1.length() - 1) {
            return true;

        }

    }

}

function validateHebrew(str1) {
    let l = str1.length();
    for (let i = 0; i < l; i++) {
        if (str1.charAt(i) >= 'א' && str1.charAt(i) <= 'ת') {
            return false;
        }
    }
    return true;
}

function validateBadChars(str1) {
    let badCh = "!#$%^&*()- <> []{}?+=~"
    let l = badCh.length();

    for (let i = 0; i < l; i++) {
        for (let j = 0; j < str1.length(); i++) {
            if (str1.charAt(j) == badCh.charAt(i)) {
                return false;
            }
        }
    }
    return true;
}