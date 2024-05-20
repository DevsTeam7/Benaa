function displayPopup(userId) {
    const popup = document.getElementById('Pup_' + userId);
    popup.style.display = 'flex';
}
function closePopup(userId) {
    const popup = document.getElementById('Pup_' + userId);
    popup.style.display = 'none';
}



