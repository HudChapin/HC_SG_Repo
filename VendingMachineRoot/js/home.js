$(document).ready(function() {

  loadItems();

 var total = 0;

  $('#addDolar').click(function (event) {
    total = parseFloat($('#totalTextbox').val());
    total += 1.00;
     $('#totalTextbox').val(total.toFixed(2));
    });

  $('#addQuarter').click(function (event) {
    total = parseFloat($('#totalTextbox').val());
    total += 0.25;
     $('#totalTextbox').val(total.toFixed(2));
    });

  $('#addDime').click(function (event) {
    total = parseFloat($('#totalTextbox').val());
    total += .10;
     $('#totalTextbox').val(total.toFixed(2));
      });

  $('#addNickel').click(function (event) {
    total = parseFloat($('#totalTextbox').val());
    total += 0.05;
     $('#totalTextbox').val(total.toFixed(2));
      });

  $('#makePurchase').click(function (event) {
     $.ajax({
         type: 'GET',
         url: 'http://localhost:8080/money/' + total.toFixed(2) + '/item/' + $('#itemTextbox').val(),
         success: function(data)
         {
           loadItems();
           $('#changeTextbox').val(data.quarters + 'quarters' + data.dimes + 'dimes' + data.nickels + 'nickles' + data.pennies + 'pennies');
           $('#totalTextbox').val('0.00');
           total = 0;
           $('#messageTextbox').val('Thanks');
         },
         error: function(xhr){
           var no = JSON.parse(xhr.responseText);
           $('#messageTextbox').val(no.message);
               }
       })
     });

    $('#changeReturn').click(function() {
      returnMoney();
      $('#itemTextbox').val('');
      $('#messageTextbox').val('');
      $('#totalTextbox').val(0.00);
      total= 0;
    });
});

function returnMoney() {
  var returnChange = $('#totalTextbox').val()*100;
  var quarter = parseInt(returnChange/25);
  quarter%=25;
  var dime = parseInt(returnChange/10);
  dime%=10;
  var nickel = parseInt(returnChange/5);
  nickel%=5;
  $('#changeTextbox').val(quarter + 'quarter(s)' + dime + 'dime(s)' + nickel + 'nickel(s)');
}

function loadItems() {
  $('#buttonItem0').empty();
  $('#buttonItem1').empty();
  $('#buttonItem2').empty();
  $('#buttonItem3').empty();
  $('#buttonItem4').empty();
  $('#buttonItem5').empty();
  $('#buttonItem6').empty();
  $('#buttonItem7').empty();
  $('#buttonItem8').empty();

  $.ajax({
    type: 'GET',
    url: 'http://localhost:8080/items',
    success: function(itemArray) {
      $.each(itemArray, function(index, item) {
        var name = item.name;
        var price = item.price.toFixed(2);
        var quantity = item.quantity;

        var item = name + '<br>' + price + '<br>' + quantity;

        $('#buttonItem' + index).append(item);
      });
    },
      error: function(xhr){
              var no = JSON.parse(xhr.responseText);
              $('#messageTextbox').val(no.message);
            }
      });
    }

function setItem(value) {
  $("#itemTextbox").val(value)
}
