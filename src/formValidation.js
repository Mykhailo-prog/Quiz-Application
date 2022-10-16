export default {
  testValidation(name) {
    if (name === "") {
      return false;
    } else {
      return true;
    }
  },
  questValidation(payload) {
    if (payload.quest === "" || payload.answer === "") {
      return false;
    } else {
      return true;
    }
  },
  answerValidation(payload) {
    if (payload.Quest.Answers.length === 0) {
      return false;
    }
    if (payload.Answer.Ans === null || payload.Answer.Ans === "") {
      return false;
    }
    let cnt = 0;
    payload.Quest.Answers.forEach((ans) => {
      if (ans.Ans === payload.Quest.CorrectAnswer) {
        cnt++;
      }
    });
    if (cnt === 1) {
      return true;
    } else {
      return false;
    }
  },
};
