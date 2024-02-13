function addToCart(productId) {
    $.ajax({
        url: '/cart/add?productId=' + productId,
        type: 'POST',
        success: function (data) {
            $('#cartModal').modal('show');
        },
        error: function (error) {
            console.error('Error adding to cart: ' + error);
        }
    });
}

function submitOrder() {
    const fullName = $('#fullName').val();
    const address = $('#address').val();
    const phone = $('#phone').val();
    const email = $('#email').val();
    const deliveryMethod = $('#deliveryMethod').val();
    const paymentMethod = $('#paymentMethod').val();
    const promoCode = $('#promoCode').val();
    const totalCost = $('#totalCost').val();

    const orderData = {
        fullName: fullName,
        address: address,
        phone: phone,
        email: email,
        deliveryMethod: deliveryMethod,
        paymentMethod: paymentMethod,
        promoCode: promoCode,
        totalCost: totalCost
    };


    fetch("/Order/OrdersToFile", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(orderData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Ответ от сервера:', data);
            window.location.href = "/Order/OrderSuccessfully";
        })
        .catch(error => {
            console.error('Произошла ошибка:', error);
        });
}


document.addEventListener("DOMContentLoaded", function () {
    const navLinks = document.querySelectorAll(".nav-link");
    const partialView = document.getElementById("partial-view");

    navLinks.forEach(function (navLink) {
        navLink.addEventListener("click", function () {
            const index = this.getAttribute("data-index");

            // Вызов компонента Administrator с передачей индекса
            fetch(`/Administrator?index=${index}`)
                .then(response => response.text())
                .then(data => {
                    partialView.innerHTML = data;
                });
        });
    });
});

$(document).ready(function () {
    $("#phone").inputmask();
});

document.querySelector('form').addEventListener('submit', function (e) {
    e.preventDefault(); // Предотвратить отправку формы по умолчанию
    const form = e.target;

    fetch(form.action, {
        method: form.method,
        headers: {
            'Content-Type': 'application/json'
        },
        body: new FormData(form)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Ответ от сервера:', data);
            window.location.href = "/Order/OrderSuccessfully";
        })
        .catch(error => {
            console.error('Произошла ошибка:', error);
        });
});