document.addEventListener("DOMContentLoaded", function () {
    function updateTable() {
        const tableName = localStorage.getItem("currentTable");
        const tableBody = document.getElementById(`${tableName}TableBody`);

        if (!tableBody) {
            console.error(`Не найден элемент с id: ${tableName}TableBody`);
            return;
        }

        console.log(`Обновление страницы для таблицы: ${tableName}`);
        
        // Запрос на получение данных с сервера
        fetch(`https://localhost:7119/api/${tableName}`)
            .then(response => response.json())
            .then(data => {
                // Очищаем текущие данные таблицы
                tableBody.innerHTML = '';

                // Перебираем полученные данные и вставляем их в таблицу
                data.forEach(row => {
                    const tr = document.createElement('tr');
                    
                    // Создаем ячейки для каждой строки данных
                    for (const key in row) {
                        if (row.hasOwnProperty(key)) {
                            const td = document.createElement('td');
                            td.textContent = row[key];
                            td.setAttribute('data-field', key); // Добавляем атрибут для поля
                            tr.appendChild(td);
                        }
                    }

                    // Добавляем кнопку редактирования в конце строки, если таблица не 'Calls' и не 'Reports'
                    if (tableName !== "Calls" && tableName !== "Reports") {
                        const editButtonTd = document.createElement('td');
                        const editButton = document.createElement('button');
                        editButton.textContent = "Редактировать";
                        editButton.classList.add("edit-button");

                        // Обработчик события для кнопки редактирования
                        editButton.addEventListener("click", function () {
                            RowEditNow(row, tr, tableName);
                        });

                        editButtonTd.appendChild(editButton);
                        tr.appendChild(editButtonTd);
                    }

                    // Добавляем строку в тело таблицы
                    tableBody.appendChild(tr);
                });
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    }

    window.updateTable = updateTable;
});

// Функция редактирования строки
function RowEditNow(rowData, rowElement, tableName) {
    console.log("Редактирование строки:", rowData);

    // Преобразуем ячейки строки в поля ввода
    const cells = rowElement.querySelectorAll('td');
    
    cells.forEach(cell => {
        if (cell.getAttribute('data-field')) {
            const field = cell.getAttribute('data-field');
            const input = document.createElement('input');
            input.type = 'text';
            input.value = rowData[field] || ''; // Заполняем значение из данных строки
            cell.textContent = ''; // Очищаем текущий текст
            cell.appendChild(input); // Добавляем поле ввода
        }
    });

    // Скрываем кнопку редактирования
    const editButton = rowElement.querySelector('.edit-button');
    editButton.style.display = 'none';

    // Создаем кнопку сохранить
    const saveButton = document.createElement('button');
    saveButton.textContent = "Сохранить";
    saveButton.classList.add("save-button");

    // Обработчик для кнопки сохранить
    saveButton.addEventListener("click", function () {
        saveRow(rowData, rowElement, tableName);
    });

    // Добавляем кнопку сохранить в ту же ячейку
    rowElement.querySelector('td:last-child').appendChild(saveButton);
}

// Функция сохранения данных
function saveRow(rowData, rowElement, tableName) {
    // Получаем ID из rowData
        let rowId;
    
        // Определяем ID строки в зависимости от таблицы
        switch (tableName) {
            case "Subscribers":
                rowId = rowData.subscriberID;
                break;
            case "Tariffs":
                rowId = rowData.tariffID;
                break;
            case "Discounts":
                rowId = rowData.discountID;
                break;
            case "Cities":
                rowId = rowData.cityID;
                break;
            default:
                console.error("Неизвестная таблица:", tableName);
                return;
        }

    
    console.log("Извлеченный ID строки:", rowId);

    if (!rowId) {
        console.error("Не найден ID для строки.");
        return;
    }

    // Получаем обновленные данные из формы
    const cells = rowElement.querySelectorAll('td');
    const updatedRowData = { ...rowData }; // Копируем исходные данные

    cells.forEach((cell, index) => {
        const input = cell.querySelector('input');
        if (input) {
            const field = cell.getAttribute('data-field'); // Если есть атрибут data-field
            if (field) {
                updatedRowData[field] = input.value; // Обновляем данные
            }
        }
    });

    // Формируем URL для запроса
    const url = `https://localhost:7119/api/${tableName}/${rowId}`;

    // Отправляем данные на сервер
    fetch(url, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(updatedRowData)
    })
    .then(data => {
        console.log("Данные успешно обновлены:", data);
        updateTable(); // Обновляем таблицу

        // Возвращаем кнопку редактирования
        rowElement.querySelector('.save-button').style.display = 'none';
        rowElement.querySelector('.edit-button').style.display = 'inline-block';
    })
    .catch((error) => {
        console.error('Ошибка при сохранении:', error);
    });
}
