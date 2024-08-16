window.addEventListener('scroll', function () {
    var header = document.getElementById('container-navbar');
    //var cont = document.getElementById('cont');
    if (window.scrollY > 500) { // Якщо користувач прокрутив сторінку вниз
        header.classList.remove('white-bg'); // Видаляємо клас з білим фоном
        header.classList.add('black-bg'); // Додаємо клас з чорним фоном
        //cont.classList.remove('unblur-bg'); // Видаляємо клас з білим фоном
        //cont.classList.add('blur-bg'); // Додаємо клас з чорним фоном
    } else { // Якщо користувач прокрутив сторінку нагору (на початок)
        header.classList.remove('black-bg'); // Видаляємо клас з чорним фоном
        header.classList.add('white-bg'); // Додаємо клас з білим фоном
        //cont.classList.remove('blur-bg'); // Видаляємо клас з чорним фоном
        //cont.classList.add('unblur-bg'); // Додаємо клас з білим фоном
        
    }
});



//const aboutLink = document.querySelector('a[href="#about"]');
//aboutLink.addEventListener('click', function (e) {
//    e.preventDefault();

//    const aboutSection = document.querySelector('#about', '#features', '#more');
//    const aboutSectionTop = aboutSection.getBoundingClientRect().top;

//    window.scrollTo({
//        top: aboutSectionTop,
//        behavior: 'smooth'
//    });
//});



//const navigationLinks = document.querySelectorAll('a[href^="#"]'); // Selects links starting with "#"
//navigationLinks.forEach(link => {
//    link.addEventListener('click', function (event) {
//        event.preventDefault(); // Prevent default jumping behavior

//        const targetSectionId = this.getAttribute('href').substring(1); // Extract target section ID
//        const targetSection = document.getElementById(targetSectionId);

//        if (targetSection) { // Ensure target section exists
//            const targetSectionTop = targetSection.getBoundingClientRect().top;
//            window.scrollTo({
//                top: targetSectionTop,
//                behavior: 'smooth'
//            });
//        } else {
//            console.error(`Section with ID "${targetSectionId}" not found.`); // Handle missing sections gracefully
//        }
//    });
//});
