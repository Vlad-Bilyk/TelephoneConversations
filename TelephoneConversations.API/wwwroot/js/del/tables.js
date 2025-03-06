document.addEventListener("DOMContentLoaded", function () {
    const tableHead = document.querySelector("thead tr");
    const tableBody = document.getElementById("callTableBody");

    // Фиксированные заголовки для каждой таблицы
    const tableHeaders = {
        "menu-1": ["", "Ідентифікатор абонента", "Назва організації", "Телефон", "Адреса", "ІПН", "Розрахунковий рахунок",""], // Абоненти
        "menu-2": ["Ідентифікатор дзвінка", "Ідентифікатор абонента", "Ідентифікатор міста", "Дата дзвінка", "Тривалість", "Час доби", "Базова вартість", "Знижка", "Кінцева вартість"], // Дзвінки
        "menu-3": ["", "Ідентифікатор тарифу", "Ідентифікатор міста", "Ціна в день", "Ціна вночі", ""], // Тарифи
        "menu-4": ["", "Ідентифікатор знижки", "Ідентифікатор тарифу", "Мінімальна тривалість", "Максимальна тривалість", "Відсоток знижки",""], // Міста
        "menu-5": ["", "Ідентифікатор міста", "Назва міста",""], // Знижки
        "menu-6": [] // Відомості (будет динамически формироваться)
    };
    fetch()
    // Заглушка данных для таблиц (заменить на запрос к БД или другие источники данных)
    const tableData = {
        "menu-1": [
            [1, "Іванов І.І.", "+380671234567", "Київ, вул. Хрещатик 1", "123456789", "UA1234567890"],
            [2, "Петров П.П.", "+380501112233", "Львів, вул. Шевченка 5", "987654321", "UA0987654321"]
        ],
        "menu-2": [
            [101, 1, 101, "2024-03-01", "3 min", "День", "$1.5", "$0.1", "$1.4"],
            [102, 2, 102, "2024-03-02", "7 min", "Ніч", "$3.0", "$0.2", "$2.8"]
        ],
        "menu-3": [
            [1, 4, "$10", "5"],
            [2, 2, "$20", "10"]
        ],
        "menu-4": [
            [1, 2, "10 min", "30 min", "15%"],
            [2, 3, "5 min", "20 min", "20%"]
        ],
        "menu-5": [
            [1, "Київ"],
            [2, "Львів"]
        ],
        "menu-6": [] // Данные для "Відомості" будут динамическими
    };



    function AddNewRow() {
        
    }

        
    // Функция обновления таблицы
    function updateTable(menuId) {
        // Очистка заголовка и тела таблицы
        tableHead.innerHTML = "";
        tableBody.innerHTML = "";

        if (!tableHeaders[menuId]) return;

        // Установка заголовков таблицы
        if (menuId === "menu-6") {
            const reportType = document.getElementById("dropdown").value;
            tableHeaders["menu-6"] = reportType === "option1"
                ? ["ID", "Абонент", "Сума", "Дата"]
                : ["ID", "Місто", "Сума", "Дата"];
        }

        tableHeaders[menuId].forEach(header => {
            const th = document.createElement("th");
            th.textContent = header;
            tableHead.appendChild(th);
        });

        // Заполняем тело таблицы
        (tableData[menuId] || []).forEach(rowData => {
            const row = document.createElement("tr");

            // Добавляем чекбокс (кроме Дзвінки и Відомості)
            if (menuId !== "menu-2" && menuId !== "menu-6") {
                const checkboxCell = document.createElement("td");
                const checkbox = document.createElement("input");
                checkbox.type = "checkbox";
                checkboxCell.appendChild(checkbox);
                row.appendChild(checkboxCell);
            }

            // Добавляем данные из массива
            rowData.forEach(cellData => {
                const td = document.createElement("td");
                td.textContent = cellData;
                row.appendChild(td);
            });

            // Добавляем кнопку "Редагувати", если не Дзвінки и Відомості
            if (menuId !== "menu-2" && menuId !== "menu-6") {
                const editCell = document.createElement("td");
                const editButton = document.createElement("button");
                editButton.textContent = "Редагувати";
                editButton.classList.add("edit-btn");
                editButton.addEventListener("click", () => editRow(row, menuId));
                editCell.appendChild(editButton);
                row.appendChild(editCell);
            }

            tableBody.appendChild(row);
        });
    }

    // Функция редактирования строки
    function editRow(row, menuId) {
        const cells = row.querySelectorAll("td:not(:first-child):not(:last-child)");
        cells.forEach(cell => {
            const input = document.createElement("input");
            input.value = cell.textContent;
            cell.innerHTML = "";
            cell.appendChild(input);
        });

        // Меняем кнопку "Редагувати" на "Зберегти"
        const editButton = row.querySelector(".edit-btn");
        editButton.textContent = "Зберегти";
        editButton.removeEventListener("click", () => editRow(row, menuId));
        editButton.addEventListener("click", () => saveRow(row, menuId));
    }

    // Функция сохранения изменений (пока просто логирует, позже заменится на API)
    function saveRow(row, menuId) {
        const cells = row.querySelectorAll("td:not(:first-child):not(:last-child) input");
        const updatedData = Array.from(cells).map(input => input.value);

        console.log(`Збережені дані (${menuId}):`, updatedData); // Тут будет отправка в БД

        cells.forEach((input, index) => {
            input.parentElement.textContent = updatedData[index];
        });

        // Меняем кнопку обратно на "Редагувати"
        const saveButton = row.querySelector(".edit-btn");
        saveButton.textContent = "Редагувати";
        saveButton.removeEventListener("click", () => saveRow(row, menuId));
        saveButton.addEventListener("click", () => editRow(row, menuId));
    }

    // Глобальный вызов функции для обновления таблицы из main.js
    window.updateTable = updateTable;
});
