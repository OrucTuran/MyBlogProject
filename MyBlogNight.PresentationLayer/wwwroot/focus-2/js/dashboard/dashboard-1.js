(function ($) {
    "use strict";

    // Morris bar chart
    $.ajax({
        url: "/Author/Dashboard/DashboardBarChart",
        method: "GET",
        success: function (response) {
            if (!response || response.length === 0) {
                console.log("Grafik için yeterli veri yok.");
                return;
            }

            // JSON verisini Morris.js'e uygun hale getir
            let formattedData = response.map(item => ({
                y: item.blogTitle.length > 20 ? item.blogTitle.substring(0, 20) + "..." : item.blogTitle,  // X ekseni (Blog baþlýðý)
                a: item.commentCount  // Y ekseni (Yorum sayýsý)
            }));

            // Morris.js Bar Chart'ý çiz
            Morris.Bar({
                element: 'morris-bar-chart',  // HTML'deki div ID
                data: formattedData,  // Backend'den gelen veri
                xkey: 'y',  // X ekseni (blog baþlýklarý)
                ykeys: ['a'],  // Y ekseni (yorum sayýsý)
                labels: ['Yorum Sayisi'],
                hideHover: 'auto',
                gridLineColor: '#eef0f2',
                resize: true,
                barColors: ['#3498db'],  // Çubuk rengi (isteðe baðlý)
                xLabelAngle: 270
            });

            // Pie chart için veriyi uygun formata çevir
            let labels = formattedData.map(item => item.y);  // Blog baþlýklarý
            let data = formattedData.map(item => item.a);  // Yorum sayýlarý
            let colors = generateColors(data.length);  // Dinamik renkleri oluþtur

            // Pie chart verisi
            let pieChartData = {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: colors.backgroundColor,
                    hoverBackgroundColor: colors.hoverBackgroundColor,
                    borderWidth: 0
                }]
            };

            // Pie chart'ý oluþtur
            var nk = document.getElementById("sold-product");  // Canvas ID'si
            new Chart(nk, {
                type: 'pie',
                data: pieChartData,
                options: {
                    responsive: true,
                    legend: {
                        display: true,  // Legend'ý göster
                        position: 'top'
                    },
                    maintainAspectRatio: false
                }
            });
        },
        error: function (error) {
            console.log("Grafik verileri yüklenirken hata oluþtu:", error);
        }
    });

    $('#info-circle-card').circleProgress({
        value: 0.70,
        size: 100,
        fill: {
            gradient: ["#a389d5"]
        }
    });

    $('.testimonial-widget-one .owl-carousel').owlCarousel({
        singleItem: true,
        loop: true,
        autoplay: false,
        autoplayTimeout: 2500,
        autoplayHoverPause: true,
        margin: 10,
        nav: false,
        dots: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });

    $('#vmap13').vectorMap({
        map: 'usa_en',
        backgroundColor: 'transparent',
        borderColor: 'rgb(88, 115, 254)',
        borderOpacity: 0.25,
        borderWidth: 1,
        color: 'rgb(88, 115, 254)',
        enableZoom: true,
        hoverColor: 'rgba(88, 115, 254)',
        normalizeFunction: 'linear',
        scaleColors: ['#b6d6ff', '#005ace'],
        selectedColor: 'rgba(88, 115, 254, 0.9)',
        selectedRegions: null,
        showTooltip: true
    });

    const wt = new PerfectScrollbar('.widget-todo');
    const wtl = new PerfectScrollbar('.widget-timeline');

    // Dinamik renk üretme fonksiyonu
    function generateColors(count) {
        let backgroundColor = [];
        let hoverBackgroundColor = [];

        // Örnek olarak her veri için bir renk üret
        for (let i = 0; i < count; i++) {
            let color = getRandomColor();  // Rastgele renk
            backgroundColor.push(color);
            hoverBackgroundColor.push(adjustColorBrightness(color, 0.2));  // Hover rengi için parlaklýk deðiþimi
        }

        return {
            backgroundColor: backgroundColor,
            hoverBackgroundColor: hoverBackgroundColor
        };
    }

    // Rastgele renk üretme fonksiyonu
    function getRandomColor() {
        let letters = '0123456789ABCDEF';
        let color = '#';
        for (let i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }

    // Rengin parlaklýðýný deðiþtiren fonksiyon (hover rengi için)
    function adjustColorBrightness(color, factor) {
        let colorRGB = hexToRgb(color);
        colorRGB.r = Math.min(255, colorRGB.r + factor * 255);
        colorRGB.g = Math.min(255, colorRGB.g + factor * 255);
        colorRGB.b = Math.min(255, colorRGB.b + factor * 255);
        return rgbToHex(colorRGB.r, colorRGB.g, colorRGB.b);
    }

    // Hex rengini RGB'ye dönüþtüren fonksiyon
    function hexToRgb(hex) {
        let r = parseInt(hex.slice(1, 3), 16);
        let g = parseInt(hex.slice(3, 5), 16);
        let b = parseInt(hex.slice(5, 7), 16);
        return { r: r, g: g, b: b };
    }

    // RGB'yi Hex'e dönüþtüren fonksiyon
    function rgbToHex(r, g, b) {
        return '#' + (1 << 24 | r << 16 | g << 8 | b).toString(16).slice(1).toUpperCase();
    }

})(jQuery);


(function($) {
    "use strict";

    var data = [],
        totalPoints = 300;

    function getRandomData() {

        if (data.length > 0)
            data = data.slice(1);

        // Do a random walk

        while (data.length < totalPoints) {

            var prev = data.length > 0 ? data[data.length - 1] : 50,
                y = prev + Math.random() * 10 - 5;

            if (y < 0) {
                y = 0;
            } else if (y > 100) {
                y = 100;
            }

            data.push(y);
        }

        // Zip the generated y values with the x values

        var res = [];
        for (var i = 0; i < data.length; ++i) {
            res.push([i, data[i]])
        }

        return res;
    }

    // Set up the control widget

    var updateInterval = 30;
    $("#updateInterval").val(updateInterval).change(function() {
        var v = $(this).val();
        if (v && !isNaN(+v)) {
            updateInterval = +v;
            if (updateInterval < 1) {
                updateInterval = 1;
            } else if (updateInterval > 3000) {
                updateInterval = 3000;
            }
            $(this).val("" + updateInterval);
        }
    });

    var plot = $.plot("#cpu-load", [getRandomData()], {
        series: {
            shadowSize: 0 // Drawing is faster without shadows
        },
        yaxis: {
            min: 0,
            max: 100
        },
        xaxis: {
            show: false
        },
        colors: ["#007BFF"],
        grid: {
            color: "transparent",
            hoverable: true,
            borderWidth: 0,
            backgroundColor: 'transparent'
        },
        tooltip: true,
        tooltipOpts: {
            content: "Y: %y",
            defaultTheme: false
        }


    });

    function update() {

        plot.setData([getRandomData()]);

        // Since the axes don't change, we don't need to call plot.setupGrid()

        plot.draw();
        setTimeout(update, updateInterval);
    }

    update();


})(jQuery);


const wt = new PerfectScrollbar('.widget-todo');
const wtl = new PerfectScrollbar('.widget-timeline');