# Solar System VR Workbench

## Architecture
AppBootstrapper -> TimeModel -> PlanetSystemController -> PlanetView

## Debug Story

### Ticket B - Event Triggered Twice After Scene Reload

**Reproduction Steps:**
When reloading the scene, a new `PlanetSystemController` subscribes to `OnTimeChanged`,
but the old subscription is not removed, causing `UpdatePlanets` to be called multiple times.

**Debugging Approach:**
1. Instrumentation: add a counter in `UpdatePlanets` to detect duplicate calls.
2. Hypothesis: `OnTimeChanged` has multiple active subscribers.
3. Verification: after scene reload, `[WARN]` logs appear.
4. Fix: add a `Dispose()` method and call it in `OnDestroy`.

**Non-Regression Test:**
If called twice in the same frame, print `[WARN]` and return early.

---

### Ticket C - Inconsistent Planet Scale, Some Planets Leave the View

**Reproduction Steps:**
With linear scaling (`pos * 2f`), Neptune (30 AU) appears at 60 Unity units,
which is completely outside the desktop display range.

**Debugging Approach:**
1. Instrumentation: log each planet's original distance and scaled distance.
2. Hypothesis: linear scaling is not suitable for the Solar System's distance range.
3. Verification: Mercury is about 0.78 units vs. Neptune about 60 units, a very large gap.
4. Fix: switch to logarithmic compression: `Log(1 + distance) * 2f`.

**Non-Regression Test:**
After scaling, print `[WARN]` if distance exceeds 15 units.
