function checkLogin(event) {
    event.preventDefault();

    const login = document.getElementById("login").value;
    const password = document.getElementById("password").value;

    const fakeUser = {
        login: "admin",
        password: "12345",
        name: "Жомір Нікіта Віталійович"
    };

    if (login === fakeUser.login && password === fakeUser.password) {
        localStorage.setItem("userName", fakeUser.name);
        localStorage.setItem("isLoggedIn", "true");
        
        
        window.location.href = "subscriber.html";
    }
}

const form = document.querySelector(".authorization-form");
form.addEventListener("submit", checkLogin);
