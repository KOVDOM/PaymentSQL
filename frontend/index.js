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