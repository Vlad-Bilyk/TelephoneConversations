function checkAuth() {
    const isLoggedIn = localStorage.getItem("isLoggedIn");

    if (isLoggedIn !== "true") {
        window.location.href = "authorization.html";
    }
}


checkAuth();