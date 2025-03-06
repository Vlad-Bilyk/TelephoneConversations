



// document.addEventListener("DOMContentLoaded", function () {
//     const menuTools = document.querySelector(".menu-tools");
//     const menuToolsTitle = document.querySelector(".menu-tools-title");
//     const dateFilterContainer = document.getElementById("date-filter-container");
    
//     function updateMenuTools(menuId) {
//         const activeItem = document.querySelector(`.item-list[data-menu-id='${menuId}']`);
//         if (activeItem) {
//             menuToolsTitle.textContent = activeItem.textContent.trim();
//         }

//         resetForm();

//         if (menuId === "menu-1") { // Абоненти
//             document.querySelector(".filter-input-1").style.display = "inline-block"; 
//             document.querySelector(".filter-form-button").style.display = "inline-block"; 
//             document.querySelector(".add").style.display = "inline-block"; 
//             document.querySelector(".delete").style.display = "inline-block"; 
//             document.querySelector(".filter-input-1").setAttribute("placeholder", "Абонент");

//         } else if (menuId === "menu-2") { // Дзвінки
//             document.querySelector(".filter-input-1").style.display = "inline-block"; 
//             document.querySelector(".filter-input-2").style.display = "inline-block"; 
//             document.querySelector(".filter-form-button").style.display = "inline-block";
//             document.querySelector(".filter-input-1").setAttribute("placeholder", "Абонент");
//             document.querySelector(".filter-input-2").setAttribute("placeholder", "Місто");

//         } else if (menuId === "menu-3") { // Тарифи
//             document.querySelector(".filter-input-1").style.display = "inline-block"; 
//             document.querySelector(".filter-form-button").style.display = "inline-block"; 
//             document.querySelector(".add").style.display = "inline-block"; 
//             document.querySelector(".delete").style.display = "inline-block"; 
//             document.querySelector(".filter-input-1").setAttribute("placeholder", "Місто");

//         } else if (menuId === "menu-4") { // Знижки
//             document.querySelector(".filter-input-1").style.display = "inline-block"; 
//             document.querySelector(".filter-form-button").style.display = "inline-block"; 
//             document.querySelector(".add").style.display = "inline-block"; 
//             document.querySelector(".delete").style.display = "inline-block"; 
//             document.querySelector(".filter-input-1").setAttribute("placeholder", "Номер тарифу");

//         } else if (menuId === "menu-5") { // Міста
//             document.querySelector(".filter-input-1").style.display = "inline-block";
//             document.querySelector(".filter-form-button").style.display = "inline-block";

//             document.querySelector(".filter-input-1").setAttribute("placeholder", "Місто");
            
//         } else if (menuId === "menu-6") { // Відомості
//             dateFilterContainer.style.display = "flex";
//             document.querySelector(".filter-form-button").style.display = "inline-block"; 
//         }
//     }

//     function resetForm() {
//         document.querySelectorAll(".filter-input").forEach(input => input.style.display = "none");
//         document.querySelector(".filter-form-button").style.display = "none";
//         document.querySelectorAll(".tools-btn").forEach(button => button.style.display = "none");
//         dateFilterContainer.style.display = "none"; // Скрываем блок с датами
//     }


//     const savedMenuId = localStorage.getItem("activeMenuItem") || "menu-1";
//     updateMenuTools(savedMenuId);


//     window.updateMenuTools = updateMenuTools;
// });
