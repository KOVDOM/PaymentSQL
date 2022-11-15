var state=false;
document.getElementById("change").onclick=function(){
    switch(state){
        case true:{
            state=!state;
            document.getElementById('id').innerHTML="";
            break;
        }
        case false:{
            state=!state;
            let contentText=`
            ID: <input type='number' min='1' value='1' id='idInput' required></input><br>
            `;
            document.getElementById('id').innerHTML=contentText;        
            break;
        }
    }
}

document.getElementById('form1').onsubmit=function (event){
    event.preventDefault();
    var nev=event.target.elements.name.value;
    var varos=event.target.elements.city.value;
    var kor=event.target.elements.age.value;

    if(state==true){
        var id=event.target.elements.idInput.value;
        var objBody=JSON.stringify({
            Id: id,
            Name: nev,
            City: varos,
            Age: kor
        });

        modifyCustList(objBody);
    }else{
        var objBody=JSON.stringify({
            Name: nev,
            City: varos,
            Age: kor
        });

        addCustList(objBody);
    }
}

async function addCustList(objBody){
    var url="http://localhost:52530/Service1.svc/putCustomer";
    var putCust=await fetch(url,{
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
    var putResult=await putCust.json();
    alert(putResult);
    console.log(putResult);
    viewCustList();
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

async function modifyCustList(objBody){
    var url="http://localhost:52530/Service1.svc/updateCustomer";
    var upCust=await fetch(url,{
        method:"POST",
        body:objBody,
        headers:{
            'content-type':"application/json"
        }
    });
    if(!upCust.ok){
        alert("Modosítási hiba!");
        return;
    }
    var upResult=await upCust.json();
    alert(upResult);
    console.log(upResult);
    viewCustList();
}

document.getElementById('delete').onclick=function(){
    var azon=document.getElementById('idDel').value;
    delCustlist(azon);
}
async function delCustlist(azon){
    var url=`http://localhost:52530/Service1.svc/deletecustomer/${azon}`;
    var delCust=await fetch(url,{
        method:"DELETE",
        headers:{
            'content-type':"application/json"
        }
    });
    if(!delCust.ok){
        alert("Törlési hiba!");
        return;
    }
    var delResult=await delCust.json();
    alert(delResult);
    console.log(delResult);
    viewCustList();
}

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