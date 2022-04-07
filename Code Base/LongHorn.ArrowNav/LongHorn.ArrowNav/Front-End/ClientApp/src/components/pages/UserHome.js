import React, { useEffect,useState } from 'react';



export function UserHome() {
    const [buildingName, setBuidlingName] = useState('');

    function fillComboBox() {
        fetch('https://localhost:44465/trafficsurvey/buildings')
            .then(response => response.json())
            .then(data => {
                console.log(data);
                var listOfBuildings = data;
                var sel = document.getElementById('buildings');
                for (var i = 0; i < listOfBuildings.length; i++) {
                    var opt = document.createElement('option');
                    opt.innerHTML = listOfBuildings[i];
                    opt.textContent = listOfBuildings[i];
                    opt.value = listOfBuildings[i];
                    sel.appendChild(opt);
                }
            })
            .catch((error) => {
                console.error('Error', error);
            });
        
    }

    function printBuilding() {
        console.log(buildingName);
    }
    useEffect(() => {
        fillComboBox();
        
    }, []);




    return (
        <>

            <div> user home place holder</div>
            <div>
                <datalist id="buildings">
                    <option>Choose a building</option>
                </datalist>
                <input autoComplete="on" list="buildings" onSelect={(e) => setBuidlingName(e.target.value)} />
                <button type="button" onClick={printBuilding}>search</button>
            </div>
        </>
    );
}