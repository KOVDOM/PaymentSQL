document.getElementById('form1').onsubmit=function (event){
    event.preventDefault();
    var nev=event.target.elements.name.value;
    var varos=event.target.elements.city.value;
    var kor=event.target.elements.age.value;

    var objBody=JSON.stringify({
        Name: nev,
        City: varos,
        Age: kor
    });

    addCustList(objBody);
}

async function addCustList(objBody){
    var url="http://localhost:52530/Service1.svc/putCustomer";
    var outCust=await fetch(url,{
        method:"POST",
        body:objBody,
        headers:{
            'content-type':"application/json"
        }
    });
    if(!putCust.ok){
        alert("POST végpont hiba!");
        return;
    }
    var putResult=putCast.json();
    console.log(putResult);
}

async function viewCustList(){
    var url="http://localhost:52530/Service1.svc/getcustomers";
    var getCust=await fetch(url,{
        method:"GET",
        headers:{
            'content-type':"application/json"
        },
    });
    var custList=await getCust.json();
    console.log(custList);
    renderCustomer(custList);
}
viewCustList();

function renderCustomer(custList){
    var contentText='';
    for (var item of custList) {
        contentText+=`<li>
        ${item.Id}
        ${item.Name}
        ${item.City}
        ${item.Age}
        </li>`;        
    }

    document.getElementById('custList1').innerHTML="<ol>"+contentText+"</ol>";
}