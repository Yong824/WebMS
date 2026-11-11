(function () {
    'use strict';

    // ─── Read config from the bridge script ───
    const config = window.SeatLayoutEditor;
    if (!config) {
        console.error('SeatLayoutEditor config not found. Check the bridge script in the view.');
        return;
    }

    const initialState = config.initialState;

    let state = {
        layoutId: initialState.layoutId,
        name: initialState.name,
        rows: initialState.totalRow,
        cols: initialState.totalColumn,
        isTemplate: initialState.isTemplate,
        hallId: initialState.hallId || null,
        cells: [],
        rowSections: [],
        sections: (initialState.sections || [
            { levelName: "Orchestra", priceModifier: 3.00, color: "#27ae60", displayOrder: 1 },
            { levelName: "Mezzanine", priceModifier: 0.00, color: "#2980b9", displayOrder: 2 },
            { levelName: "Balcony", priceModifier: -2.00, color: "#8e44ad", displayOrder: 3 }
        ]).sort((a, b) => {
            const order = { "Orchestra": 1, "Mezzanine": 2, "Balcony": 3 };
            return (order[a.levelName] || 99) - (order[b.levelName] || 99);
        }),
        currentTool: "Seat",
        currentCategory: "Standard"
    };

    // Convert server cells to 2D grid
    const cellMap = {};
    (initialState.cells || []).forEach(c => { cellMap[`${c.row}-${c.col}`] = c; });

    for (let r = 0; r < state.rows; r++) {
        state.cells[r] = [];
        let foundSection = null;

        for (let c = 0; c < state.cols; c++) {
            const existing = cellMap[`${r}-${c}`];
            if (existing) {
                state.cells[r][c] = { type: existing.type, category: existing.category, section: existing.sectionLevel, label: existing.label };
                if (existing.sectionLevel && !foundSection) foundSection = existing.sectionLevel;
            } else {
                state.cells[r][c] = { type: "Empty" };
            }
        }
        // Default row section to Orchestra if any seat has a section, otherwise default to Orchestra
        state.rowSections[r] = foundSection || null;
    }

    // ─── RENDER GRID ───
    function renderGrid() {
        const grid = document.getElementById('seatGrid');
        // 👈 Add an extra 40px column for the Row Headers
        grid.style.gridTemplateColumns = `40px repeat(${state.cols}, 32px)`;
        grid.innerHTML = '';

        for (let r = 0; r < state.rows; r++) {
            // 1. Render Row Header (Section Indicator)
            const sec = state.rowSections[r];
            const secObj = state.sections.find(s => s.levelName === sec);
            const secClass = sec ? `section-${sec.toLowerCase()}` : 'section-none';
            const secChar = sec === "Orchestra" ? "O" : sec === "Mezzanine" ? "M" : sec === "Balcony" ? "B" : "-";

            const header = document.createElement('div');
            header.className = `row-header ${secClass}`;
            header.innerHTML = secChar;
            header.title = `Row ${r + 1}: ${sec || 'No Section'}`;
            header.addEventListener('mousedown', (e) => { e.preventDefault(); sectionPainting = true; cycleRowSection(r); });
            header.addEventListener('mouseenter', () => { if (sectionPainting) setRowSection(r, activeSectionBrush); });
            grid.appendChild(header);

            // 2. Render Seats
            for (let c = 0; c < state.cols; c++) {
                const cell = state.cells[r][c];
                const div = document.createElement('div');
                div.id = `cell-${r}-${c}`;
                div.className = getCellClass(cell);
                div.title = cell.label || `${r},${c}`;

                div.addEventListener('mousedown', (e) => { e.preventDefault(); painting = true; paint(r, c); });
                div.addEventListener('mouseenter', () => { if (painting) paint(r, c); });

                grid.appendChild(div);
            }
        }
    }

    function getCellClass(cell) {
        let cls = `cell type-${cell.type.toLowerCase()}`;
        if (cell.type === "Seat" && cell.category && cell.category !== "Standard") {
            cls += ` ${cell.category.toLowerCase()}`;
        }
        return cls;
    }

    // ─── PAINT LOGIC ───
    let painting = false;
    let sectionPainting = false;
    let activeSectionBrush = "Orchestra"; // Default section for dragging

    document.addEventListener('mouseup', () => { painting = false; sectionPainting = false; });

    function paint(r, c) {
        if (state.currentTool === "Seat") {
            state.cells[r][c] = {
                type: "Seat",
                category: state.currentCategory,
                section: state.rowSections[r], // 👈 Inherit from Row Header
                label: `R${r + 1}C${c + 1}`
            };
        } else {
            state.cells[r][c] = { type: state.currentTool, label: state.currentTool };
        }

        const div = document.getElementById(`cell-${r}-${c}`);
        if (div) div.className = getCellClass(state.cells[r][c]);
    }

    // ─── ROW SECTION LOGIC ───
    function cycleRowSection(r) {
        const secs = ["Orchestra", "Mezzanine", "Balcony", null];
        let current = state.rowSections[r];
        let nextIdx = (secs.indexOf(current) + 1) % secs.length;
        activeSectionBrush = secs[nextIdx]; // Set brush for dragging
        setRowSection(r, activeSectionBrush);
    }

    function setRowSection(r, sectionName) {
        state.rowSections[r] = sectionName;

        // Update all seats in this row to match the new section
        for (let c = 0; c < state.cols; c++) {
            if (state.cells[r][c].type === "Seat") {
                state.cells[r][c].section = sectionName;
            }
        }
        renderGrid(); // Re-render to update colors
    }

    // ─── PALETTE ───
    document.querySelectorAll('.palette-btn').forEach(btn => {
        btn.addEventListener('click', () => {
            document.querySelectorAll('.palette-btn').forEach(b => b.classList.remove('active'));
            btn.classList.add('active');
            state.currentTool = btn.dataset.tool;
            state.currentCategory = btn.dataset.cat || "Standard";
        });
    });

    function renderSections() {
        const list = document.getElementById('sectionList');
        list.innerHTML = '';
        state.sections.forEach((s, i) => {
            const div = document.createElement('div');
            div.className = 'mb-1 p-2 border rounded';
            div.style.background = (s.color || "#000") + '22';
            div.innerHTML = `
                            <div><strong>${s.levelName}</strong></div>
                            <input type="number" step="0.01" value="${s.priceModifier}" class="form-control form-control-sm mt-1"
                                   onchange="state.sections[${i}].priceModifier = parseFloat(this.value)">
                        `;
            list.appendChild(div);
        });
    }

    // ─── ADD/REMOVE ROWS & COLUMNS ───
    function addRow() {
        state.rows++;
        state.cells.push(new Array(state.cols).fill(null).map(() => ({ type: "Empty" })));
        state.rowSections.push(null); // Default new row to Orchestra
        renderGrid();
    }
    function addCol() {
        state.cols++;
        state.cells.forEach(row => row.push({ type: "Empty" }));
        renderGrid();
    }
    function removeRow() {
        if (state.rows <= 1) return;
        state.rows--;
        state.cells.pop();
        state.rowSections.pop();
        renderGrid();
    }
    function removeCol() {
        if (state.cols <= 1) return;
        state.cols--;
        state.cells.forEach(row => row.pop());
        renderGrid();
    }

    document.getElementById('btnAddRow').addEventListener('click', addRow);
    document.getElementById('btnAddCol').addEventListener('click', addCol);
    document.getElementById('btnRemoveRow').addEventListener('click', removeRow);
    document.getElementById('btnRemoveCol').addEventListener('click', removeCol);

    // ─── SAVE ───
    document.getElementById('btnSave').addEventListener('click', async () => {
        state.name = document.getElementById('layoutName').value;

        const cells = [];
        for (let r = 0; r < state.rows; r++) {
            for (let c = 0; c < state.cols; c++) {
                const cell = state.cells[r][c];
                if (cell.type === "Empty") continue;
                cells.push({
                    row: r, col: c,
                    type: cell.type,
                    category: cell.category || null,
                    sectionLevel: cell.section || null,
                    label: cell.label
                });
            }
        }

        const dto = {
            layoutId: state.layoutId,
            name: state.name,
            totalRow: state.rows,
            totalColumn: state.cols,
            isTemplate: state.isTemplate,   // ← read from state instead of hardcoded
            hallId: state.hallId || null,       // TODO, allow admin to add layout to a hall
            sections: state.sections.map((s, i) => ({ ...s, displayOrder: i + 1 })),
            cells: cells
        };

        const res = await fetch('/SeatLayout/Save', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify(dto)
        });

        if (res.ok) {
            alert("Template saved successfully!");
            window.location.href = '/SeatLayout';
        } else {
            const err = await res.json();
            alert("Save failed: " + (err.message || "Unknown error"));
        }
    });

    // ─── BOOT ───
    renderSections();
    renderGrid();
})();