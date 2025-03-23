let k;
let p_amount;
const map_size_x = 10000;
const map_size_y = 10000;
let all_points = [];
let clasters = [];
const canvas1 = document.getElementById("canvas1");
const ctx1 = canvas1.getContext("2d");
const canvas2 = document.getElementById("canvas2");
const ctx2 = canvas2.getContext("2d");
let scale_x = canvas1.width / map_size_x;  
let scale_y = canvas1.height / map_size_y;

function startAlgorithm() {
    const k = parseInt(document.getElementById("k").value);
    const p_amount = parseInt(document.getElementById("p_amount").value);

    if (isNaN(k) || isNaN(p_amount) || k < 1 || p_amount < 1000 || k > 20 || p_amount > 100000) {
        alert("Пожалуйста, введите корректные значения для K и N.");
        return;
    }

    start(k, p_amount);

}

function start(k, p_amount){
    all_points = []; 
    clasters = [];
    init(k, p_amount);
    iteration(k, p_amount);
    drawClusters(ctx1);
    KMeans(k, p_amount);
    drawClusters(ctx2);
}

function KMeans(k, p_amount){
    let isChange;
    do{
        isChange = false;

        clasters.forEach(claster => claster.points = []);

        iteration(k, p_amount);

        for(let i = 0; i < k; i++){
            let temp_center_x = clasters[i].center.x;
            let temp_center_y = clasters[i].center.y;
            newCentroid(clasters[i]);
            if(temp_center_x !== clasters[i].center.x || temp_center_y !== clasters[i].center.y){
                isChange = true;
            }

        }

    }while(isChange)
}

function newCentroid(claster){
    if (claster.points.length === 0) return;

    let all_x = 0;
    let all_y = 0;
    for(let point of claster.points){
        all_x += point.x;
        all_y += point.y;
    }
    claster.center.x = all_x/claster.points.length;
    claster.center.y = all_y/claster.points.length;
}

function iteration(k, p_amount){
    for(let i = 0; i<p_amount;i++){
        let point = all_points[i];
        let min = Infinity;
        let min_index = 0;
        for(let j = 0; j < k; j++){
            if(min > distance(clasters[j].center,point)){
                min = distance(clasters[j].center,point);
                min_index = j;
            }
        }   
        clasters[min_index].points.push(point);

    }

}

function init(k, p_amount){
    
    //init all points
    for(let i = 0; i < p_amount; i++){
        all_points.push({
            x: randomInteger(0,map_size_x),
            y: randomInteger(0,map_size_y),
        });
    }
    
    //init clasters
    for(let i = 0; i < k; i++){
        let rand_point = all_points[randomInteger(0,p_amount-1)];
        clasters.push({
            center: {
                x: rand_point.x,
                y: rand_point.y,
            },
            points: [],
            color: color(randomInteger(0,255), randomInteger(0,255), randomInteger(0,255))
        });

    }
    
}

function distance(center, point){
    return Math.hypot(center.x - point.x, center.y - point.y);
}

function randomInteger(min, max) {
    let rand = min + Math.random() * (max + 1 - min);
    return Math.floor(rand);
}

function color(r, g, b) {
    return `rgb(${r},${g},${b})`;
}

function drawClusters(ctx) {
    ctx.clearRect(0, 0, canvas1.width, canvas1.height);
    clasters.forEach(cluster => {
        ctx.fillStyle = cluster.color;
        cluster.points.forEach(point => {
            ctx.fillRect(point.x * scale_x, point.y * scale_y, 2, 2);
        });

        ctx.fillStyle = "red";
        ctx.beginPath();
        ctx.arc(cluster.center.x * scale_x, cluster.center.y * scale_y, 5, 0, Math.PI * 2);
        ctx.fill();
    });
}

//start(k, p_amount);