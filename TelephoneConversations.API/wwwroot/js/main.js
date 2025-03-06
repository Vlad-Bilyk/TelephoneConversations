function logout() {
    localStorage.removeItem("isLoggedIn");
    localStorage.removeItem("activeMenuItem");
    localStorage.removeItem("selectedTable");

    window.location.href = "authorization.html";
}

document.addEventListener("DOMContentLoaded", function () {
    // Обработчик для выхода
    document.getElementById("logout-button").addEventListener("click", logout);
    
    

    // Устанавливаем имя пользователя из localStorage
    document.querySelector(".user-name").textContent = localStorage.getItem("userName");

    // Определяем таблицу на основе текущего меню
    let tableName = document.querySelector(".menu-tools-title").textContent;
    switch (tableName) {
        case "Абоненти":
            localStorage.setItem("currentTable", "Subscribers");
            editRow();
            updateTable();
            break;
        case "Дзвінки":
            localStorage.setItem("currentTable", "Calls");
            updateTable();
            break;
        case "Тарифи":
            localStorage.setItem("currentTable", "Tariffs");
            editRow();
            updateTable();
            break;
        case "Знижки":
            localStorage.setItem("currentTable", "Discounts");
            editRow();
            updateTable();
            break;
        case "Міста":
            localStorage.setItem("currentTable", "Cities");
            editRow();
            updateTable();
            break;
        case "Відомості":
            localStorage.setItem("currentTable", "Reports");
            break;
    }
});

function AddRow() {
    // Получаем форму
    const form = document.querySelector(".add-window-form");
    if (!form) {
        console.error("Форма не найдена.");
        return;
    }

    // Получаем данные формы с помощью FormData
    const formData = new FormData(form);
    const formDataObj = {};
    let isFormValid = true;

    // Перебираем все элементы формы и проверяем на заполненность
    for (let [key, value] of formData.entries()) {
        value = value.trim(); // Убираем лишние пробелы
        const input = form.querySelector(`[name="${key}"]`);

        if (value === "") {
            isFormValid = false;
            if (input) {
                input.style.borderColor = "red"; // Подсвечиваем незаполненные поля
            }
        } else {
            if (input) {
                input.style.borderColor = ""; // Сбрасываем подсветку
            }
            formDataObj[key] = value;
        }
    }

    if (!isFormValid) {
        alert("Пожалуйста, заполните все поля.");
        return;
    }

    // Получаем название текущей таблицы из localStorage
    const currentTable = localStorage.getItem("currentTable");
    if (!currentTable) {
        console.error("Текущая таблица не определена.");
        return;
    }

    // Формируем URL для API
    const apiUrl = `https://localhost:7119/api/${currentTable}`;

    // Отправляем данные через API
    fetch(apiUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(formDataObj)
    })
    .then(response => {
        // Проверяем статус ответа
        if (!response.ok) {
            throw new Error(`Ошибка сети: ${response.statusText}`);
        }
        return response.text(); // Читаем как текст
    })
    .then(data => {
        console.log("Ответ от сервера:", data);  // Логируем ответ от сервера

        // Пытаемся парсить данные, если они есть
        try {
            const jsonResponse = JSON.parse(data);
            console.log("Данные успешно добавлены:", jsonResponse);

            // Закрыть окно и очистить форму
            const addWindow = document.querySelector(".add-window");
            if (addWindow) {
                addWindow.style.display = "none";
            }
            form.reset();

            // Обновить таблицу (если функция updateTable существует)
            if (typeof updateTable === "function") {
                updateTable();
            }
        } catch (error) {
            console.error("Ошибка при парсинге JSON:", error);
        }
    })
    .catch(error => {
        console.error("Ошибка:", error);
    });
}



function editRow() {
    console.log("EditRow - on");

    // Открыть окно для добавления записи
    document.getElementById("tools-btn-add").addEventListener("click", function () {
        document.querySelector(".add-window").style.display = "flex";
    });

    // Обработчик для кнопки "Добавить"
    document.querySelector(".add-window-btn").addEventListener("click", function () {
        AddRow(); // Вызываем функцию добавления записи
    });

    // Закрытие окна и очистка формы
    document.querySelector(".close-window-btn").addEventListener("click", function () {
        document.querySelector(".add-window-form").reset();
        document.querySelector(".add-window").style.display = "none";
    });

    // Обработчик для кнопки "Удалить запись"
    document.getElementById("tools-btn-delete").addEventListener("click", function () {
        const rowId = prompt("Введите идентификатор записи для удаления:");
        if (rowId) {
            deleteRow(rowId);
        }
    });
}

// Функция для удаления строки
function deleteRow(rowId) {
    const currentTable = localStorage.getItem("currentTable");
    if (!currentTable) {
        console.error("Текущая таблица не определена.");
        return;
    }

    const apiUrl = `https://localhost:7119/api/${currentTable}/${rowId}`;
    fetch(apiUrl, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then(data => {
        console.log("Запись успешно удалена:", data);
        if (typeof updateTable === "function") {
            updateTable();
        }
    })
    .catch(error => {
        console.error("Ошибка при удалении записи:", error);
    });
}


