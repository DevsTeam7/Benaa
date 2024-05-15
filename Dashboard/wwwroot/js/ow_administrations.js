
var counter = 0; 

function addAdmin(e) {
    var name = document.getElementById('name').value;
    var email = document.getElementById('email').value;
    var password = document.getElementById('password').value;

    if (name === '' || email === '' || password ==='') {
        alert('خطأ');
        e.preventDefault();
        return;
    }
    if (!validateEmail(email)) {
        alert('الرجاء ادخال بريد الكتروني صحيح');
        e.preventDefault();
        return;
    }

    if (!validatePassword(password)) {
        alert('Password must contain at least one uppercase letter, one lowercase letter, and one number.');
        alert('كلمة المرور يجب ان تحتوي على حروف وارقام ');
        e.preventDefault();
        return;
    }

    var admin = {
        name: name,
        email: email,
        password: password
    };

    var adminsTable = document.getElementById('admins');
    var adminRow = document.createElement('tr');
    counter++;
    adminRow.innerHTML = `
    <td>${counter}</td>
    <td>${admin.name}</td>
    <td>${admin.email}</td>
    <td>${admin.password}</td>
    <td>
        <a href="" class="btn btn-warning">تعديل</a>
        <button class="btn btn-danger" onclick="deleteAdmin(this)">حذف</button>
    </td>
`;
    adminsTable.appendChild(adminRow);

    document.getElementById('name').value = '';
    document.getElementById('email').value = '';
    document.getElementById('password').value = '';

 
    updateCounter();
}

function deleteAdmin(button) {
    var row = button.parentNode.parentNode;
    row.parentNode.removeChild(row);
    counter--; 

    updateCounter();
}

function updateCounter() {
    var rows = document.querySelectorAll('#admins tr');
    for (var i = 0; i < rows.length; i++) {
        var cell = rows[i].querySelector('td:first-child');
        cell.textContent = i + 1; 
    }
}

function validateEmail(email) {
    var emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    return emailRegex.test(email);
}

function validatePassword(password) {
    var uppercaseRegex = /[A-Z]/;
    var lowercaseRegex = /[a-z]/;
    var numberRegex = /[0-9]/;
    return uppercaseRegex.test(password) && lowercaseRegex.test(password) && numberRegex.test(password);
}
