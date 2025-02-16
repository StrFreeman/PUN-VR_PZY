﻿/*
 * Copyright (C) 2012-2023 MotionSystems
 *
 * This file is part of ForceSeatMI SDK.
 *
 * www.motionsystems.eu
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MySceneLoader : MonoBehaviour
{
    [MenuItem("Scenes/Open Main Scene")]
    static void OpenMainScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/TablePhyPos_Unity.unity");
    }
}
