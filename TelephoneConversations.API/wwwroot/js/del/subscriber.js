document.addEventListener("DOMContentLoaded", function () {
    // Додати запис
    document.getElementById("tools-btn-add").addEventListener("click", function () {
        document.querySelector(".add-window").style.display = "flex";
        
        document.querySelector(".add-window-btn").addEventListener("click", function () {

            console.log("Отправка даних")

        });
        document.querySelector(".close-window-btn").addEventListener("click", function () {
            document.querySelector(".add-window-form").reset();
            document.querySelector(".add-window").style.display = "none";
        });
    });

    // Видалити запис
    document.getElementById("tools-btn-delete").addEventListener("click", function () {
        prompt("Введіть ідентифікатор рядка:")
    });

});