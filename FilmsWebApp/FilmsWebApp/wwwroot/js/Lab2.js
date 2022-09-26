const uri = 'api/Directors';
let directors = [];

function getDirectors() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayDirectors(data))
        .catch(error => console.error('Unable to get directors.', error));
}

function addDirector() {
    const addNameTextbox = document.getElementById('add-name');
    const addCountryTextbox = document.getElementById('add-country');

    const director = {
        name: addNameTextbox.value.trim(),
        country: addCountryTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(director)
    })
        .then(response => response.json())
        .then(() => {
            getDirectors();
            addNameTextbox.value = '';
            addCountryTextbox.value = '';
        })
        .catch(error => console.error('Unable to add director.', error));
}

function deleteDirector(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getDirectors())
        .catch(error => console.error('Unable to delete director.', error));
}

function displayEditForm(id) {
    const director = directors.find(director => director.id === id);

    document.getElementById('edit-id').value = director.id;
    document.getElementById('edit-name').value = director.name;
    document.getElementById('edit-country').value = director.country;
    document.getElementById('editForm').style.display = 'block';
}

function updateDirector() {
    const directorId = document.getElementById('edit-id').value;
    const director = {
        id: parseInt(directorId, 10),
        name: document.getElementById('edit-name').value.trim(),
        country: document.getElementById('edit-country').value.trim()
    };

    fetch(`${uri}/${directorId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(director)
    })
        .then(() => getDirectors())
        .catch(error => console.error('Unable to update director.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}


function _displayDirectors(data) {
    const tBody = document.getElementById('directors');
    tBody.innerHTML = '';


    const button = document.createElement('button');

    data.forEach(director => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${director.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteDirector(${director.id})`);

        let tr = tBody.insertRow();


        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(director.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(director.country);
        td2.appendChild(textNodeInfo);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    directors = data;
}