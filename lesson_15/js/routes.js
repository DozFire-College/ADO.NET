async function loadStudents() {

    const table = document.getElementById('studentsTable');
    table.style = "display: flex;";

    const tbody = document.querySelector('#studentsTable tbody');
    tbody.innerHTML = '';

    const res = await fetch('/students/all');

    const students = await res.json();

    for (const s of students) {
        const tr = document.createElement('tr');
        tr.innerHTML = `
                    <th>${s.studentId}</th>
                    <th>${s.firstName}</th>
                    <th>${s.lastName}</th>
                    <th>${s.age}</th>
                `;
        tbody.appendChild(tr);
    }
}
