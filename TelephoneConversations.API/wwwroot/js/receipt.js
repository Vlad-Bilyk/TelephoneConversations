function receiptGeneration() {
    const id = document.getElementById("receipt-id").value;
    const fromDate = document.getElementById("receipt-from-date").value;
    const toDate = document.getElementById("receipt-to-date").value;

    fetch(`/api/Invoices/${id}/download?fromDate=${fromDate}&toDate=${toDate}`, {
        method: "GET",
        headers: {
            "Accept": "application/pdf"
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error("Ошибка при получении PDF");
        }
        return response.blob(); // Преобразуем в файл
    })
    .then(blob => {
        if (blob.size === 0) {
            throw new Error("Получен пустой файл!");
        }

        const url = window.URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = `invoice_${id}.pdf`; // Имя скачиваемого файла
        document.body.appendChild(a);
        a.click();
        window.URL.revokeObjectURL(url);
        document.body.removeChild(a);
    })
    .catch(error => {
        console.error("Ошибка:", error);
        alert(error.message);
    });
}



document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("receipt-button").addEventListener("click", function () {
        document.querySelector(".receipt-window").style.display = "flex";
    });

    document.querySelector(".create-receipt-button").addEventListener("click", function (event) {
        event.preventDefault(); // Отменяем стандартную отправку формы

        const inputs = document.querySelectorAll(".receipt-window-form input");
        let isValid = true;

        inputs.forEach(input => {
            if (!input.value.trim()) {
                input.style.border = "2px solid red";
                isValid = false;
            } else {
                input.style.border = "";
            }
        });

        if (isValid) {
            receiptGeneration();
        }
    });

    // Закрытие окна и очистка формы
    document.querySelector(".close-receipt-button").addEventListener("click", function () {
        document.querySelector(".receipt-window-form").reset();
        document.querySelectorAll(".receipt-window-form input").forEach(input => {
            input.style.border = ""; // Убираем красную рамку
        });
        document.querySelector(".receipt-window").style.display = "none";
    });
});
