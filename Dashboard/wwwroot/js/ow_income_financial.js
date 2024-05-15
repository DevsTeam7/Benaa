      // الحصول على مرجع للعناصر
      const infoBtn = document.getElementById('infoBtn');
      const popup = document.getElementById('popup');
      const closeBtn = document.getElementById('closeBtn');
    
      // إضافة حدث النقر للزر المعلومات
      infoBtn.addEventListener('click', function() {
        popup.style.display = 'flex';
      });
    
      // إضافة حدث النقر لزر إغلاق النافذة
      closeBtn.addEventListener('click', function() {
        popup.style.display = 'none';
      });


    