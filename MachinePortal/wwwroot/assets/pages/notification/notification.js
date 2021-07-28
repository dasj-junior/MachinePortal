function notify(title, message, from, align, icon, type, animIn, animOut) {
    $.growl({
        icon: icon,
        title: title,
        message: message,
        url: ''
    }, {
        element: 'body',
        type: type,
        allow_dismiss: true,
        placement: {
            from: from,
            align: align
        },
        offset: {
            x: 36,
            y: 110
        },
        spacing: 10,
        z_index: 999999,
        delay: 3500,
        timer: 1000,
        url_target: '_blank',
        mouse_over: false,
        animate: {
            enter: animIn,
            exit: animOut
        },
        icon_type: 'class',
        template: '<div style="width:30%;" data-growl="rt-container" role="alert">' +
            '<div class="col-rt-6">' +
            '<div class="flag note note--' + type + '">' +
            '<div class="flag__image note__icon">' +
                '<i class="' + icon + '"></i>' +
            '</div>' +
            '<div class="flag__body note__text">' +
                '<span data-growl="message"></span>' +
            '</div>' +
            '<a href="#" data-growl="dismiss" class="note__close">' +
            '<i class="bi-x"></i>' +
            '</a>' +
            '</div>' +
            '</div>' +
            '</div>'
    });
};