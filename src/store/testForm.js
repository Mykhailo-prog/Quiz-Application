export default {
  state: {
    test: {
      name: null,
      questions: [
        {
          question: null,
          correctAnswer: null,
          answers: [{ answer: "", questionId: null }],
        },
      ],
    },
    question: {
      answers: [{ answer: "", questionId: null }],
    },
    answer: {},
  },
  mutations: {},
  getters: {
    Test: (state) => state.test,
    Question: (state) => state.question,
    Answer: (state) => state.answer,
  },
  actions: {
    addAnswer({ state }, payload) {
      state.test.questions[payload.QuestPos].answers[payload.AnswerPos] =
        payload.Answer;
    },
    AnswerCount({ state }, payload) {
      if (payload.Param === "+") {
        state.test.questions[payload.Pos].answers.push(payload.Param);
        console.log(state.test.questions[payload.Pos].answers.length);
      } else if (payload.Param === "-") {
        state.test.questions[payload.Pos].answers.pop();
        console.log(payload.Param);
      }
    },
  },
};
