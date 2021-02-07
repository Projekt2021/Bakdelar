// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function ListProduct() {
    const queryString = window.location.search;
    let productID = new URLSearchParams(queryString).get('id');

    let url = "https://localhost:44347/api/Products/" + productID;
    fetch(url)
        .then(function (response) {
            return response.json();
        })
        .then(function (product) {
            console.log(product.name);
            document.title = product.name + " - Bakdelar.se";
        })
        .catch(err => { throw err });
}