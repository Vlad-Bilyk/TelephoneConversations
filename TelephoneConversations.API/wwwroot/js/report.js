document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector(".filter-form");
    const tableBody = document.getElementById("ReportsTableBody");

    if (!form) return;

    form.addEventListener("submit", async function (event) {
        event.preventDefault();

        if (!tableBody) {
            console.error("Элемент ReportsTableBody не найден в DOM.");
            return;
        }

        const fromDateInput = document.getElementById("from-date");
        const toDateInput = document.getElementById("to-date");
        const dropdownInput = document.getElementById("dropdown");

        if (!fromDateInput || !toDateInput || !dropdownInput) {
            console.error("Один или несколько элементов формы не найдены.");
            return;
        }

        const fromDate = fromDateInput.value;
        const toDate = toDateInput.value;
        const dropdownValue = dropdownInput.value;

        if (!fromDate || !toDate) {
            console.error("Оба поля даты должны быть заполнены.");
            return;
        }

        let endpoint = "";
        if (dropdownValue === "option1") {
            endpoint = "bySubscribers";
        } else if (dropdownValue === "option2") {
            endpoint = "byCities";
        }

        const queryParams = new URLSearchParams({
            fromDate,
            toDate
        }).toString();

        try {
            const response = await fetch(`/api/Reports/${endpoint}?${queryParams}`, {
                method: "GET"
            });

            if (!response.ok) {
                throw new Error("Ошибка при получении данных с сервера");
            }

            const result = await response.json();
            console.log("Ответ от сервера:", result);

            renderTable(result);
        } catch (error) {
            console.error("Ошибка запроса:", error);
        }
    });

    function renderTable(data) {
        if (!tableBody) {
            console.error("Элемент ReportsTableBody отсутствует в DOM.");
            return;
        }
        
        if (!Array.isArray(data) || data.length === 0) {
            tableBody.innerHTML = "<tr><td colspan='100%'>Немає даних для відображення</td></tr>";
            return;
        }

        const tableHead = document.querySelector(".table-wrapper thead tr");
        if (tableHead) {
            tableHead.innerHTML = Object.keys(data[0]).map(key => `<th>${key}</th>`).join('');
        }

        tableBody.innerHTML = data.map(row => `<tr>${Object.values(row).map(value => `<td>${value}</td>`).join('')}</tr>`).join('');
    }
});