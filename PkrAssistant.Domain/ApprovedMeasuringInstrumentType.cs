namespace PkrAssistant.Domain
{
    /// <summary>
    /// Утверждённый тип средства измерений
    /// </summary>
    public class ApprovedMeasuringInstrumentType
    {
        public Guid Id { get; private set; }
        public string RegistrationNumber { get; private set; }
        public int? VerificationIntervalYears { get; private set; }

        // Внешние ключи
        public Guid MeasuringInstrumentTypeId { get; private set; }
        public Guid VerificationMethodId { get; private set; }

        // Навигационные свойства
        public MeasuringInstrumentType Type { get; private set; }
        public VerificationMethod Method { get; private set; }

        public ICollection<MeasuringInstrumentModification> Modifications { get; private set; }
        public ICollection<MeasuringInstrument> Instruments { get; private set; }

        // Для EF
        private ApprovedMeasuringInstrumentType() {}

        public ApprovedMeasuringInstrumentType(
            string registrationNumber, 
            Guid measuringInstrumentTypeId,
            Guid verificationMethodId,
            int? verificationIntervalYears = null)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber) == true)
            {
                throw new ArgumentException("Регистрационный номер не может быть пустым", nameof(registrationNumber));
            }

            if (measuringInstrumentTypeId == Guid.Empty)
            {
                throw new ArgumentException("Тип средства измерений должен быть указан", nameof(measuringInstrumentTypeId));
            }

            if (verificationMethodId == Guid.Empty)
            {
                throw new ArgumentException("Методика поверки должна быть указана", nameof(verificationMethodId)); 
            }

            Id = Guid.NewGuid();

            RegistrationNumber = registrationNumber.Trim();

            VerificationIntervalYears = verificationIntervalYears;
            MeasuringInstrumentTypeId = measuringInstrumentTypeId;

            VerificationMethodId = verificationMethodId;

            Modifications = new List<MeasuringInstrumentModification>();
            Instruments = new List<MeasuringInstrument>();
        }

    }
}
