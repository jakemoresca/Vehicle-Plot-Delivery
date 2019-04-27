# Scenario: `Get average number of queries can handle per second`

- Duration time: `00:01:00`
- RPS: `112`
- Concurrent Copies: `1`

| __step__                 | __details__                                     |
|--------------------------|-------------------------------------------------|
| name                     | `Query vehicle plots`                           |
| request count            | all = `6730`, OK = `6730`, failed = `0`         |
| response time            | RPS = `112`, min = `3`, mean = `8`, max = `49`  |
| response time percentile | 50% = `9`, 75% = `10`, 95% = `11`, StdDev = `2` |
| data transfer            | min = - , mean = - , max = - , all = -          |