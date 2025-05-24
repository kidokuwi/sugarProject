
function validateForm(event) {
    return true;
    let isPassValid = checkPass();
    let isEmailValid = checkEmail();
    if (!isEmailValid || !isPassValid) {
        event.preventDefault();
        event.stopPropagation();
    }
    

    return isEmailValid && isPassValid;
}

function checkEmail() {
    let errorEmail = document.getElementById("reg_errormail");
    const emailElement = document.getElementById("reg_email");
    const email = emailElement.value;

    let errorMsg = [];

    if (!checkAtSymbol(email))
        errorMsg.push("invalid or multiple @ .");
    if (!checkDot(email))
        errorMsg.push("Invalid dot placement.");
    if (!checkLen(email))
        errorMsg.push("Email too short.");
    if (!validateBadChars(email))
        errorMsg.push("Invalid characters.");
    if (!validateHebrew(email))
        errorMsg.push("No Hebrew characters allowed.");

    if (errorMsg.length > 0) {
        errorEmail.innerHTML = errorMsg.join("<br>");
        errorEmail.style.display = "block";
        emailElement.classList.add("is-invalid");
        emailElement.classList.remove("is-valid");
        return false;
    } else {
        errorEmail.innerHTML = "";
        errorEmail.style.display = "none";
        emailElement.classList.remove("is-invalid"); //remove invalid icon
        emailElement.classList.add("is-valid"); // add Bootstrap valid icon
        return true;
    }
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
    return true;
    let errorEmail = document.getElementById("reg_errormail");
    let badCh = "!#$%^&*()-<>[]{}?+=~'"
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


function checkPass() {
    const password = document.getElementById("reg_password");
    const errorpass = document.getElementById("reg_errorpass");

 

    let hasUpperCase = false;
    let hasLowerCase = false;
    let hasNumber = false;

    for (let i = 0; i < password.value.length; i++) {
        const char = password.value.charAt(i);
        if (char >= 'A' && char <= 'Z') hasUpperCase = true;
        if (char >= 'a' && char <= 'z') hasLowerCase = true;
        if (char >= '0' && char <= '9') hasNumber = true;
    }


    if (password.value.length < 6) {
        errorpass.innerHTML = "Password too short";
        errorpass.style.display = "block";
        password.classList.remove("is-valid");
        password.classList.add("is-invalid");
        return false;
    }
    else if (password.value.length > 20) {
        errorpass.innerHTML = "Password too long";
        errorpass.style.display = "block";
        password.classList.add("is-invalid");
        password.classList.remove("is-valid");
        return false;

    }
    else if (!hasUpperCase) {
        errorpass.innerHTML = "Password must contain at least one capital letter";
        errorpass.style.display = "block";
        password.classList.add("is-invalid");
        password.classList.remove("is-valid");
        return false;
    } else if (!hasLowerCase) {
        errorpass.innerHTML = "Password must contain at least one non-capital letter";
        errorpass.style.display = "block";
        password.classList.add("is-invalid");
        password.classList.remove("is-valid");
        return false;
    } else if (!hasNumber) {
        errorpass.innerHTML = "Password must contain at least one number";
        errorpass.style.display = "block";
        password.classList.add("is-invalid");
        password.classList.remove("is-valid");
        return false;
    }
    else if (!validateBadChars(password.value)) {
        errorpass.innerHTML = "Invalid characters";
        errorpass.style.display = "block";
        password.classList.add("is-invalid");
        password.classList.remove("is-valid");
        return false;
    }
    else {
        errorpass.innerHTML = "";
        errorpass.style.display = "none";
        password.classList.remove("is-invalid");
        password.classList.add("is-valid");
    }
  
    return true;
    
}
