using QuanLyCanHoVaCuDan.Data;
using QuanLyCanHoVaCuDan.Repositories;
using QuanLyCuDan.Model;

namespace QuanLyCanHoVaCuDan.DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly QuanLyCanHoVaCuDanContext context;
        private GenericRepository<Apartment> apartmentRepository;
        private CitizenRepository citizenRepository;
        private CitizenApartmentRepository citizenApartmentRepository;


        public UnitOfWork(QuanLyCanHoVaCuDanContext context)
        {
            this.context = context;
        }

       

        public GenericRepository<Apartment> ApartmentRepository
        {
            get
            {
                if (this.apartmentRepository == null)
                {
                    this.apartmentRepository = new GenericRepository<Apartment>(context);
                }
                return apartmentRepository;
            }
        }
        public CitizenRepository CitizenRepository
        {
            get
            {
                if (this.citizenRepository == null)
                {
                    this.citizenRepository = new CitizenRepository(context);
                }
                return citizenRepository;
            }
        }
        public CitizenApartmentRepository CitizenApartmentRepository
        {
            get
            {
                if (this.citizenApartmentRepository == null)
                {
                    this.citizenApartmentRepository = new CitizenApartmentRepository(context);
                }
                return citizenApartmentRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
