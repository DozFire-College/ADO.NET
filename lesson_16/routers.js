async function loadStudents() {
    ClearPage();
    const table = document.getElementById('studentsTable');
    table.style.display = "table";
    

    const tbody = table.querySelector('tbody');
    tbody.innerHTML = '';

    const res = await fetch('/students/all');

    const students = await res.json();

    for (const s of students) {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td> ${s.studentId}</td>
            <td> ${s.firstName}</td>
            <td> ${s.lastName}</td>
            <td> ${s.age}</td>
        `;
        tbody.appendChild(tr);
    }
}


async function loadGroups() {
    ClearPage();
    const table = document.getElementById('groupsTable');
    table.style.display = "table";


    const tbody = table.querySelector('tbody');
    tbody.innerHTML = '';

    const res = await fetch('/groups/all');

    const groups = await res.json();

    for (const g of groups) {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td> ${g.groupId}</td>
            <td> ${g.groupName}</td>
        `;
        tbody.appendChild(tr);
    }
}

async function findGroupsById(id) {
    ClearPage();

    const table = document.getElementById('studentsTable');
    const tbody = table.querySelector('tbody');
    const caption = table.querySelector('caption');
    const status = document.getElementById('loadStatus');

    tbody.replaceChildren();
    status.textContent = 'Загрузка…';

    try {
        const response = await fetch(`/groups/${id}`, {
            method: 'POST'
        });

        if (response.status === 404) {
            status.textContent = 'Группа не найдена.';
            return;
        }

        if (!response.ok) {
            throw new Error(`Ошибка HTTP: ${response.status}`);
        }

        const group = await response.json();

        if (!group) {
            status.textContent = 'Группа не найдена.';
            return;
        }

        if (!Array.isArray(group.students)) {
            throw new Error('Сервер не вернул массив students.');
        }

        caption.textContent = `Студенты группы ${group.groupName}`;

        for (const student of group.students) {
            const row = document.createElement('tr');

            for (const value of [
                student.studentId,
                student.firstName,
                student.lastName,
                student.age
            ]) {
                const cell = document.createElement('td');
                cell.textContent = value ?? '';
                row.appendChild(cell);
            }

            tbody.appendChild(row);
        }

        table.style.display = 'table';

        status.textContent = group.students.length === 0
            ? 'В этой группе пока нет студентов.'
            : '';
    } catch (error) {
        status.textContent = 'Не удалось загрузить студентов группы.';
        console.error(error);
    }
}



function ClearPage() {
    const tg = document.getElementById('groupsTable');
    const ts = document.getElementById('studentsTable');
    ts.style = "display: none;"
    tg.style = "display: none;"
}