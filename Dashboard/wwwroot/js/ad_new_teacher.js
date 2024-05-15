  // الحصول على مرجع للعناصر
  const infoBtn1 = document.getElementById('infoBtn1');
  const popup1 = document.getElementById('popup1');
  const closeBtn1 = document.getElementById('closeBtn1');

  // إضافة حدث النقر للزر المعلومات
  infoBtn1.addEventListener('click', function() {
    popup1.style.display = 'flex';
  });

  // إضافة حدث النقر لزر إغلاق النافذة
  closeBtn1.addEventListener('click', function() {
    popup1.style.display = 'none';
  });


