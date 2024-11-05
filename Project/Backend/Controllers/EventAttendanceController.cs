        // POST: Gebruiker inschrijven voor een evenement
        [HttpPost("attend")]
        public IActionResult AttendEvent([FromBody] AttendanceDto attendance)
        {
            // Simulatie van een controle voor inloggen (vervang met een echte authenticatiecheck)
            bool isLoggedIn = true; // Vervang dit met een echte inlogcheck

            // Controleer of de gebruiker is ingelogd
            if (!isLoggedIn)
            {
                return Unauthorized("Gebruiker is niet ingelogd.");
            }

            // Zoek de gebruiker en het evenement in de database
            var user = _context.Users.FirstOrDefault(u => u.Id == attendance.UserId);
            var eventEntity = _context.Events.FirstOrDefault(e => e.Id == attendance.EventId);

            // Controleer of de gebruiker of het evenement niet bestaat
            if (user == null || eventEntity == null)
            {
                return NotFound("Gebruiker of evenement niet gevonden.");
            }

            // Controleer of het evenement al is gestart of voorbij is
            if (eventEntity.Date.ToDateTime(eventEntity.StartTime) < DateTime.Now)
            {
                return BadRequest("Het evenement is al begonnen of voorbij.");
            }

            // Controleer of de gebruiker al voor dit evenement is ingeschreven
            var existingAttendance = _context.Attendance
                .FirstOrDefault(a => a.UserId == attendance.UserId && a.Date == eventEntity.Date);

            if (existingAttendance != null)
            {
                return BadRequest("U heeft zich al ingeschreven voor dit evenement.");
            }

            // Voeg een nieuwe inschrijving toe
            var newAttendance = new Attendance
            {
                UserId = attendance.UserId,
                Date = eventEntity.Date // Stel de datum van het evenement in als de inschrijvingsdatum
            };

            // Sla de nieuwe inschrijving op in de database
            _context.Attendance.Add(newAttendance);
            _context.SaveChanges();

            return Ok("Inschrijving succesvol geregistreerd.");
        }
