namespace PkrAssistant.Domain
{
    /// <summary>
    /// Модификация типа средства измерений
    /// </summary>
    public class MeasuringInstrumentModification
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int? VerificationIntervalYears { get; private set; }

        // Внешний ключ
        public Guid ApprovedMeasuringInstrumentTypeId { get; private set; }

        // Навигационное свойство
        public ApprovedMeasuringInstrumentType ApprovedType { get; private set; }

        // для EF
        public MeasuringInstrumentModification() {}

        public MeasuringInstrumentModification(
            string name,
            Guid approvedMeasuringInstrumentTypeId,
            int? verificationIntervalYears = null)
        {
            if (string.IsNullOrWhiteSpace(name) == true)
            {
                throw new ArgumentException("Наименование модификации должно быть указано", nameof(name));
            }

            if (approvedMeasuringInstrumentTypeId == Guid.Empty) 
            { 
                throw new ArgumentException("Тип утверждённого средства измерений должен быть указан", nameof(approvedMeasuringInstrumentTypeId));
            }

            Id = Guid.NewGuid();
            Name = name.Trim();

            VerificationIntervalYears = verificationIntervalYears;
            ApprovedMeasuringInstrumentTypeId = approvedMeasuringInstrumentTypeId;
        }
    }
}
