
namespace com.teleca.fleetonline.web.bean
{
    /// <summary>
    /// Aids is retriving the answer from the server based on a response.
    /// </summary>
    [System.Serializable]
    public class AnswerHelper
    {

        private string answer = "failed";

        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }
        public const string STATUS_WAIT = "tryAgain";

        public AnswerHelper()
        {
        }

        public AnswerHelper(bool success)
        {
            if (success)
            {
                answer = "success";
            }
        }
        public AnswerHelper(string key)
        {
            answer = key;
        }

        public sealed override string ToString()
        {
            return @get();
        }

        public string @get()
        {
            return Answer;
        }
    }
}