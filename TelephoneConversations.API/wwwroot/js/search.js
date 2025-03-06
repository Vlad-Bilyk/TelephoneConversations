document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".filter-form").forEach(form => {
        form.addEventListener("submit", function (event) {
            event.preventDefault();
            
            let tableName = localStorage.getItem("currentTable");
            let filterValue1 = this.querySelector("#filter-form-1").value.trim();
            let filterValue2 = this.querySelector("#filter-form-2")?.value.trim();
            console.log(tableName)
            applyFilter(tableName, filterValue1, filterValue2);
        });
    });
});

function applyFilter(tableName, filterValue1, filterValue2) {
    let apiUrl = "https://localhost:7119/api/" + tableName + "/search";
    let query = "";

    console.log(tableName)
    switch (tableName) {
        case "Subscribers":
            query = `?companyName=${encodeURIComponent(filterValue1)}`;
            break;
        case "Tariffs":
            query = `?cityId=${encodeURIComponent(filterValue1)}`;
            break;
        case "Discounts":
            query = `?tariffId=${encodeURIComponent(filterValue1)}`;
            break;
        case "Cities":
            query = `?cityName=${encodeURIComponent(filterValue1)}`;
            break;
        case "Calls":
            query = `?subscriberName=${encodeURIComponent(filterValue1)}&cityName=${encodeURIComponent(filterValue2 || "")}`;
            break;
        default:
            console.error("Фильтрация для данной таблицы не поддерживается.");
            return;
    }
    console.log(apiUrl + query);
    fetch(apiUrl + query, {
        method: "GET",
        headers: { "Content-Type": "application/json" }
    })
    .then(response => response.json())
    .then(data => {
        updateTableWithFilteredData(data);console.log(data);
    })
    .catch(error => console.error("Ошибка при фильтрации данных:", error));
    
    console.log(tableName)
}

function updateTableWithFilteredData(filteredData) {
    let tableBody = document.querySelector("table tbody");
    tableBody.innerHTML = "";
    
    filteredData.forEach(rowData => {
        let row = document.createElement("tr");
        
        Object.values(rowData).forEach(value => {
            let cell = document.createElement("td");
            cell.textContent = value;
            row.appendChild(cell);
        });
        
        tableBody.appendChild(row);
    });
}