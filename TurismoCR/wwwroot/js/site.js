function showDates() {
    $("#selectBirthDate").pickadate({
        monthsFull: [
            "enero", "febrero", "marzo", "abril", 
            "mayo", "junio", "julio", "agosto", 
            "septiembre", "octubre", "noviembre", "diciembre"
        ],
        monthsShort: [
            "ene", "feb", "mar", "abr", 
            "may", "jun", "jul", "ago", 
            "sep", "oct", "nov", "dic"
        ],
        weekdaysFull: [
            "domingo", "lunes", "martes", "miércoles", 
            "jueves", "viernes", "sábado"
        ],
        weekdaysShort: [
            "dom", "lun", "mar", 
            "mié", "jue", "vie", "sáb"
        ],
        today: "hoy",
        clear: "borrar",
        close: "cerrar",
        firstDay: 1,
        format: "yyyy-mm-dd",
    });

    $("#selectServiceStartDate").pickadate({
        monthsFull: [
            "enero", "febrero", "marzo", "abril", 
            "mayo", "junio", "julio", "agosto", 
            "septiembre", "octubre", "noviembre", "diciembre"
        ],
        monthsShort: [
            "ene", "feb", "mar", "abr", 
            "may", "jun", "jul", "ago", 
            "sep", "oct", "nov", "dic"
        ],
        weekdaysFull: [
            "domingo", "lunes", "martes", "miércoles", 
            "jueves", "viernes", "sábado"
        ],
        weekdaysShort: [
            "dom", "lun", "mar", 
            "mié", "jue", "vie", "sáb"
        ],
        today: "hoy",
        clear: "borrar",
        close: "cerrar",
        firstDay: 1,
        format: "yyyy-mm-dd",
        min: [2017, 9, 12]
    });

    $("#selectServiceEndDate").pickadate({
        monthsFull: [
            "enero", "febrero", "marzo", "abril", 
            "mayo", "junio", "julio", "agosto", 
            "septiembre", "octubre", "noviembre", "diciembre"
        ],
        monthsShort: [
            "ene", "feb", "mar", "abr", 
            "may", "jun", "jul", "ago", 
            "sep", "oct", "nov", "dic"
        ],
        weekdaysFull: [
            "domingo", "lunes", "martes", "miércoles", 
            "jueves", "viernes", "sábado"
        ],
        weekdaysShort: [
            "dom", "lun", "mar", 
            "mié", "jue", "vie", "sáb"
        ],
        today: "hoy",
        clear: "borrar",
        close: "cerrar",
        firstDay: 1,
        format: "yyyy-mm-dd",
        min: [2017, 9, 12]
    });
}

jQuery(
    showDates
)
