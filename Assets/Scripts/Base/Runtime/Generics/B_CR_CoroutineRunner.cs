using System.Threading.Tasks;
namespace Base {
    public class B_CR_CoroutineRunner : B_M_ManagerBase {
        public static B_CR_CoroutineRunner instance;
        public B_CR_CoroutineQueue CQ;

        public override Task ManagerStrapping() {
            if (instance == null) instance = this;
            else Destroy(gameObject);
            CQ = new B_CR_CoroutineQueue(this);
            CQ.StartLoop();
            return base.ManagerStrapping();
        }

        public override Task ManagerDataFlush() {
            instance = null;
            return base.ManagerDataFlush();
        }
    }
}