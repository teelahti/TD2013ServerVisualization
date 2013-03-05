/// <reference path="../Scripts/d3.v3.js" />

var CounterChart = function (counter, rootElement) {
    if (!rootElement) {
        throw new Error("Must give a valid DOM element for root");
    }
    
    if (!counter) {
        throw new Error("Must give valid counter object");
    }

    var h2 = document.createElement("h2");
    h2.innerText = counter.Name;
    rootElement.appendChild(h2);

    this.update = tick;


    // D3.js chart implementation
    // Based on https://gist.github.com/mbostock/1642874
    var n = 40,
        data = createZeroFilledArray(n);
        // data = d3.range(n);

    var margin = { top: 10, right: 10, bottom: 20, left: 40 },
        width = 800 - margin.left - margin.right,
        height = 250 - margin.top - margin.bottom;

    var x = d3.scale.linear()
        .domain([0, n - 1])
        .range([0, width]);

    var y = d3.scale.linear()
        .domain([0, 100])
        .range([height, 0]);

    var line = d3.svg.line()
        .x(function (d, i) { return x(i); })
        .y(function (d, i) { return y(d); });

    var svg = d3.select(rootElement)
        .append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    svg.append("defs").append("clipPath")
        .attr("id", "clip")
        .append("rect")
        .attr("width", width)
        .attr("height", height);

    svg.append("g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + height + ")")
        .call(d3.svg.axis().scale(x).orient("bottom"));

    svg.append("g")
        .attr("class", "y axis")
        .call(d3.svg.axis().scale(y).orient("left"));

    var path = svg.append("g")
        .attr("clip-path", "url(#clip)")
        .append("path")
        .data([data])
        .attr("class", "line")
        .attr("d", line);

    function tick(val) {

        // push a new data point onto the back
        // data.push(random());
        data.push(val);

        // redraw the line, and slide it to the left
        path
            .attr("d", line)
            .attr("transform", null)
            .transition()
            .duration(200)
            .ease("linear")
            .attr("transform", "translate(" + x(-1) + ")");
            //.each("end", tick);

        // pop the old data point off the front
        data.shift();
    }
    
    // Start with 0
    tick(0);
    
    function createZeroFilledArray(count) {
        var a = [];
        for (var i = count; i > 0; i--) {
            a.push(0);
        }

        return a;
    };
};