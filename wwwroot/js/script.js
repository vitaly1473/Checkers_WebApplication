/* file wwwroot/js/script.js */


/* ������ ��� �������� ����� */

document.addEventListener("DOMContentLoaded", function () {
    const board = document.getElementById('board');
    const board0 = document.getElementById('board0');

    const rowLabelsContainer = document.querySelector('.row-labels');
    const colLabelsContainer = document.querySelector('.col-labels');


    const size = 8;
    const cellColors = ['white', 'black'];

    // Add cells to the board 1 ������� 
    //for (let row = 0; row < size; row++) {
    //    for (let col = 0; col < size; col++) {
    //        const cell = document.createElement('div');
    //        const colorIndex = (row + col) % 2;
    //        cell.className = `cell ${cellColors[colorIndex]}`;
    //        board0.appendChild(cell);
    //    }
    //}

    // ���������� ����� �������� (A-H) ������
    for (let col = 0; col < size; col++) {
        const colLabel = document.createElement('div');
        colLabel.className = 'col-label';       
        colLabel.textContent = String.fromCharCode(65 + col); /* ������� ������ A,B,C... */
        colLabelsContainer.appendChild(colLabel);
    }

    // ���������� ����� �� ����� � ����� ����� (1-8) �����
    for (let row = 0; row < size; row++)  
    {
        const rowLabel = document.createElement('div');
        rowLabel.className = 'row-label';
        rowLabel.textContent = size - row; // 1.2.3, ... /* ����� ������� ����� 1,2,3... */
        rowLabelsContainer.appendChild(rowLabel);
    
        // ���������� �����-����� ����� �� ����� 2 �������
        for (let col = 0; col < size; col++)
        {
            const cell = document.createElement('div');
            const colorIndex = (row + col) % 2;
            cell.className = `cell ${cellColors[colorIndex]}`;

            // ������ ����������� ����� �� �����
            if (colorIndex === 1 && (row < 3 || row > 4)) /* ���� ������ ������ - �� ������ �� �� ����� */
            {
                const checker = document.createElement('div');
                checker.className = row < 3 ? 'checker black' : 'checker white';
                cell.appendChild(checker);
            }

            board.appendChild(cell);
        }
    }

});