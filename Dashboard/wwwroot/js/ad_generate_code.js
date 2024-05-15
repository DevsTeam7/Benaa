var generatedCodes = [];
  
function generateCodes() {
  var quantityInput = document.getElementById('quantity-input');
  var amountInput = document.getElementById('amount-input');
  var codeList = document.getElementById('code-list');

  var quantity = parseInt(quantityInput.value);
  var amount = parseInt(amountInput.value);

  codeList.innerHTML = ''; // مسح المحتوى السابق
  generatedCodes = []; // مسح الأكواد المولدة

  for (var i = 0; i < quantity; i++) {
    var codeElement = document.createElement('div');
    var code = generateRandomCode();
    codeElement.textContent =  
     "مبلغ الشحن  :"+amount + " --- "+"  كود الشحن :"  + code  ;
    codeList.appendChild(codeElement);
    generatedCodes.push(codeElement.textContent);
  }
}

function generateRandomCode() {
  var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  var code = '';

  for (var i = 0; i < 10; i++) {
    var randomIndex = Math.floor(Math.random() * characters.length);
    code += characters.charAt(randomIndex);
  }

  return code;
}

function exportToText() {
  var text = generatedCodes.join('\n');
  var blob = new Blob([text], { type: 'text/plain' });
  var url = URL.createObjectURL(blob);
  var link = document.createElement('a');
  link.href = url;
  link.download = 'codes.txt';
  link.click();
}

function clearContent() {
  var codeList = document.getElementById('code-list');
  codeList.innerHTML = '';
  generatedCodes = [];
}